using app_products.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Services

//GetTypes("Services", "Service").ToList().ForEach(type => services.TryAddScoped(type.Key, type.Value));

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static IDictionary<Type, Type> GetTypes(string nameSpace, string endWith, params string[] exceptions)
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