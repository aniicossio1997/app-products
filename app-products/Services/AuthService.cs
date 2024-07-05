using app_products.Exceptions;
using app_products.Repositories.IRepositories;
using app_products.Services.IServices;
using app_products.ViewModels;
using app_products.ViewModels.ClassesBase;
using app_products.ViewModels.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace app_products.Services
{
    public class AuthService : IAuthService
    {

        private readonly ILogger<AuthService> _logger;
        private readonly IUserRepository _repository;
        private string _tokenSecret;

        public AuthService(IUserRepository repository,
            IConfiguration config, ILogger<AuthService> logger
             )
        {
            _tokenSecret = config.GetSection("Jwt:Key").Value;
            _repository = repository;
            _logger = logger;
        }


        public async Task<UserSessionViewModel> LogIn(LoginViewModel loginRequest)
        {
            try
            {
                var user = await _repository.GetFirstByFilter(new UserFilterViewModel { UserName = loginRequest.Username });
                if (user == null)
                {
                    throw new EntityNotFoundException();
                }

                var password = EncriptPassword(loginRequest.Password);
                if (password != user.Password)
                {
                    throw new EntityNotFoundException();
                }
                var token = GenerateJwtToken(user);
                var session = new UserSessionViewModel { Username = user.Username, Token = token };
                return session;
            }catch(Exception ex)
            {
                var result=ex.Message;
                throw new EntityNotFoundException();
            }
        }

        public async Task<UserViewModel> SignUp(RegistrationViewModel signupData)
        {
            
                _logger.LogInformation(signupData.ToString());

                signupData.Password = EncriptPassword(signupData.Password);

                return await _repository.Save(signupData);
            
        }

        private string EncriptPassword(string rawPassword)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(rawPassword));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        private string GenerateJwtToken(UserViewModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSecret));
            var claims = new List<Claim>
            {
                new Claim("username", user.Username),
                new Claim("id", user.Id.ToString()),
            };
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
