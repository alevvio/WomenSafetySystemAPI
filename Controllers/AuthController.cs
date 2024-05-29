using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WomenSafetySystemApi.Models.DTO;
using Microsoft.AspNetCore.Identity;
using WomenSafetySystemApi.Repositories;

namespace WomenSafetySystemApi.Controllers;

[ApiController]
[Route("auth")]

public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly ITokenRepository tokenRepository;
    public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
        this.userManager = userManager;
        this.tokenRepository = tokenRepository;
    }

    //Register - hostname.com/auth/Register
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
    {
        var IdentityUser = new IdentityUser
        {
            UserName = registerRequestDTO.Username,
            Email = registerRequestDTO.Username,
        };

        var identityResult = await userManager.CreateAsync(IdentityUser, registerRequestDTO.Password);

        if (identityResult.Succeeded)
        {
            //Add Roles to this User
            if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
            {
                identityResult = await userManager.AddToRoleAsync(IdentityUser, registerRequestDTO.Roles);

                if (identityResult.Succeeded)
                {
                    return Ok("User was registered successfully! Please Login.");
                }
            }
        }
        return BadRequest("Something Went Wrong");
    }

    //Login - hostname.com/auth/Login
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
    {
        var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);

        if(user != null)
        {
            var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (checkPasswordResult)
            {
                //Get Roles for the User
                var roles = await   userManager.GetRolesAsync(user);

                //Create Token
                if (roles != null)
                {
                    var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                    
                    var response = new LoginResponseDTO
                    {
                        JwtToken = jwtToken
                    };
                    
                    return Ok(response);
                }             
            }
        }
        return BadRequest("Username or password incorrect");
    }
}