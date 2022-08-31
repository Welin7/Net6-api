using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPIs.Models;
using WebAPIs.Token;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateTokenIdentity")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if(string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return Unauthorized();
            }

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                //Recovery logon user
                var usercurrent = await _userManager.FindByEmailAsync(login.Email);
                var userId = usercurrent.Id;

                var token = new TokenJwtBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                .AddSubject("Welin - Api Net6 Core")
                .AddIssuer("Teste.Securiry.Bearer")
                .AddAudience("Teste.Securiry.Bearer")
                .AddClaim("userId", userId)
                .AddExpiry(5)
                .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUserIdentity")]
        public async Task<IActionResult> AddUserIdentity([FromBody] Login login)
        {
           if(string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return Ok("Miss something data");

            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                Cpf = login.Cpf,
                Type = TypeUser.Common,
            };

            var result = await _userManager.CreateAsync(user, login.Password);

            if(result.Errors.Any())
            {
                return Ok(result.Errors);
            }

            // Generation Confirmation case needed
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // return email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultEmailConfirmation = await _userManager.ConfirmEmailAsync(user, code);

            if (resultEmailConfirmation.Succeeded)
                return Ok("Add user with sucess");
            else
                return Ok("Error confirming user");
        }
    }
}
