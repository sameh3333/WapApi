using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WapApi.Services;
using BL.DTOs;
using BL.Contracts;
using Domines;
using System.Security.Cryptography.Xml;

namespace WapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokinService _tokenService;
        private readonly IUserService _userService;
        private readonly IRefreshTokens _refreshTokens;
        private readonly IRefreshTokensRetriver _refreshTokensRetriver;
        public AuthController(
            TokinService tokenService,
           IUserService userService,
         IRefreshTokensRetriver refreshTokens)
        {
            
          _tokenService = tokenService;
            _userService = userService;
            _refreshTokensRetriver = refreshTokens;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto model)
        {

            var result = await _userService.RegisterAsync(model);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDtos  model)
        {
            var userregister = await _userService.LoginAsync(model);
            if (!userregister.Success)
            {
                return Unauthorized("Invalid email or password.");
            }// var result = await _userService.GetUserByEmailAsync(model.Email);

            
            var UserData  = await GetClimis(model.Email );
            var claims = UserData.Item1;
            UserDto user = UserData.Item2;

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Save refreshToken to cookie
            var stsrtedToken = new RefreshTokensDTOs
            {
                Token = refreshToken,
                UserId = user.Id.ToString(),
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                CurrentState = 1
            };


            // Save refreshToken to cookie
            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            // Optional: Save refreshToken to DB linked to user

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("refresh-access-token")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized("Refresh token is missing");
            }
            // TODO: Verify refreshToken from DB and get user

            // Simulated user for example:
            var storedToken = _refreshTokensRetriver.GetByToken(refreshToken);
            if (storedToken == null||storedToken.CurrentState==2|| storedToken.ExpiryDate<DateTime.UtcNow)
                return Unauthorized("Invaild Or Expried Refrach token  ");

           // var user = await _userManager.FindByIdAsync(storedToken.UserId);
            var claims = await GetClimisById(storedToken.UserId);


            var newAccessToken = _tokenService.GenerateAccessToken(claims);
            return Ok(new { AccessToken = newAccessToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {

            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized("Refresh token is missing");
            }
            // TODO: Verify refreshToken from DB and get user
            var storedToken = _refreshTokensRetriver.GetByToken(refreshToken);
            if (storedToken == null || storedToken.CurrentState == 2 || storedToken.ExpiryDate < DateTime.UtcNow)
                return Unauthorized("Invaild Or Expried Refrach token  ");

            var claims = await GetClimisById(storedToken.UserId);


            var newAccessToken = _tokenService.GenerateAccessToken(claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var NewstsrtedToken = new RefreshTokensDTOs
            {
                Token = refreshToken,
                UserId = storedToken.Id.ToString(),
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                CurrentState = 1
            };

            _refreshTokens.Refresh(NewstsrtedToken);

            Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            // TODO: Save new refresh token to DB and remove old one

            return Ok(new { AccessToken = newAccessToken });
        }
        async  Task<(Claim[]?, UserDto)> GetClimis(string email ) {
            var result = await _userService.GetUserByEmailAsync(email);
            var claims = new[] {
             new Claim(ClaimTypes.Name,result.Email),
             new Claim(ClaimTypes.Role,"User"),
            };

            return (claims, result);

        }
        async Task<Claim[]> GetClimisById(string UserId)
        {
            var result = await _userService.GetUserByIdAsync(UserId);

            var claims = new[] {
             new Claim(ClaimTypes.Name,result.Email),
             new Claim(ClaimTypes.Role,"User"),
            };

            return claims;

        }




    }

    
}
