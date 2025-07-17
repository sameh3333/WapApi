using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BL.DTOs;
using BL.Contracts;
using DAL.Models;
using System.Security.Claims;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHttpContextAccessor _IHttpContextAccessor;
    private readonly IRefreshTokensRetriver _IRefreshTokens;        

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        IHttpContextAccessor iHttpContextAccessor, IRefreshTokensRetriver iRefreshTokens)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _IHttpContextAccessor = iHttpContextAccessor;
        _IRefreshTokens = iRefreshTokens;
    }

    public async Task<UserRegusterDto> RegisterAsync(UserDto registerDto)
    {
        if (registerDto.Password != registerDto.ConfirmPassword)
        {
            return new UserRegusterDto
            {
                Success = false,
                Errors = new[] { "password do not match" }

            };

        }
        //Ckesk in NMAEand email in vailde   CreateAsync Ctreta Passward . Haching
        var user = new ApplicationUser { UserName = registerDto.Email, Email = registerDto.Email };
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        return new UserRegusterDto
        {
            Success = result.Succeeded,
            Errors = result.Errors?.Select(e => e.Description)
        };
    }

    public async Task<UserRegusterDto> LoginAsync(LoginDtos loginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, true, false);

        if (!result.Succeeded)
        {
            return new UserRegusterDto
            {
                Success = false,
                Errors = new[] { " Invaild Login attempt." }
            };
        }
        return new UserRegusterDto { Success = true, Token = "DummyTokenForNow" };
    }

    public async Task LogoutAsenc()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<UserDto> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return null;

        return new UserDto
        {
            Id = Guid.Parse(user.Id),
            Email=user.Email,
        };
    }
    public async Task<UserDto> GetUserByEmailAsync(string Email)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user == null) return null;

        return new UserDto
        {
            Id = Guid.Parse(user.Id),
            Email = user.Email,
        };
    }

    public async Task<IEnumerable<UserDto>> GetUserByIdAsync()
    {
        var users = _userManager.Users;
        return users.Select(u => new UserDto {
            Id= Guid.Parse(u.Id),   
            Email=u.Email,
        });


    }
    public Guid GetLoggedInServices()
    {
        var refarshtoken = _IHttpContextAccessor.HttpContext?.Request.Cookies["RefreshToken"];
       // var userId = _IHttpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
       var refrsshToken = _IRefreshTokens.GetByToken(refarshtoken);
        return Guid.Parse(refrsshToken.UserId);
    }
}
