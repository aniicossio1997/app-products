using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using app_products.Services.IServices;
using app_products.ViewModels;
using System.Threading;

namespace app_products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SignUp(RegistrationViewModel signupData, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Ok(await _authService.SignUp(signupData));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginViewModel loginRequest)
        {
            var res =await _authService.LogIn(loginRequest);
            return Ok(res);
        }
    }
}
