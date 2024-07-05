using app_products.ModelConfigurations;
using app_products.Services.IServices;
using app_products.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using app_products.Repositories.IRepositories;
using app_products.Repositories;
using app_products.Exceptions.Handler;
using app_products.Configuration;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.OpenApi.Models;


namespace app_products
{
    public static class Startup
    {
        public static WebApplication InitApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            ConfigureServices(builder);
            var app=builder.Build();
            Configure(app);
            return app;
        }
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add services to the container.

            #region Auth

            builder.Configuration.AddJsonFile("appsettings.json");
            var secretKey = builder.Configuration.GetSection("Jwt:Key").Value;
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken=true;
                config.TokenValidationParameters=new TokenValidationParameters
                {
                    IssuerSigningKey=new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer=false,
                    ValidateAudience=false,
                };
            });
            #endregion

            #region
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllers()
                .AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())); ;
            #endregion

            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Services
            //builder.Services.AddTransient<ICategoriesService, CategoriesService>();
            //builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();


            builder.Services.AddServices("app_products.Services.IServices", "app_products.Services");
            builder.Services.AddServices("app_products.Repositories.IRepositories", "app_products.Repositories");




            #endregion

            #region CORS
            builder.Services.AddCors(options => options.AddPolicy("AllowWepapp",
                                       builder => builder.AllowAnyOrigin()
                                      .AllowAnyHeader().AllowAnyMethod()));
            #endregion


            #region config SQL

            var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql
                (connectionString, ServerVersion.AutoDetect(connectionString));

            });

            #endregion

            #region Swagger
            builder.Services.AddSwaggerGen(option =>
            {
                            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                            {
                                In = ParameterLocation.Header,
                                Description = "Please enter a valid token",
                                Name = "Authorization",
                                Type = SecuritySchemeType.Http,
                                BearerFormat = "JWT",
                                Scheme = "Bearer"
                            });
                            option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                        });
            #endregion
        }

        public static void Configure(WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseErrorHandling(); // Agregar el middleware de manejo de errores


            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            //app.Run();

        }


        public static IServiceCollection ConfigureServices(IServiceCollection services, string connectionString)
        {
            #region Services

            GetTypes("Services", "Service").ToList().ForEach(type => services.TryAddScoped(type.Key, type.Value));

            #endregion

            #region Repositories

            GetTypes("Repositories", "Repository").ToList().ForEach(type => services.TryAddScoped(type.Key, type.Value));

            #endregion


            return services;
        }

        private static IDictionary<Type, Type> GetTypes(string nameSpace, string endWith, params string[] exceptions)
        {
            var res = new Dictionary<Type, Type>();
            var executingAssembly = Assembly.GetExecutingAssembly();
            if (executingAssembly == null)
            {
                throw new InvalidOperationException("Unable to get the entry assembly.");
            }
            var assemblyTypes = executingAssembly.GetTypes();
            foreach (var typeImplementation in assemblyTypes
                .Where(p => p.Name.EndsWith(endWith))
                .Where(p => !exceptions.Contains(p.Name))
                .Where(t => string.Equals(t.Namespace, executingAssembly.GetName().Name + "." + nameSpace, StringComparison.Ordinal)))
            {
                var interfaceType = typeImplementation.GetInterface("I" + typeImplementation.Name);
                if (interfaceType != null)
                {
                    res.Add(interfaceType, typeImplementation);
                }
            }

            return res;
        }

    }
}
