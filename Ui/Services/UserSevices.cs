using Microsoft.AspNetCore.Identity;
using BL.DTOs;
using BL.Contracts;
using DAL.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Data;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHttpContextAccessor _IHttpContextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor iHttpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _IHttpContextAccessor = iHttpContextAccessor;
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
        
        var user = new ApplicationUser { UserName = registerDto.Email, Email = registerDto.Email ,FirstName=registerDto.FirstName
        ,LastName=registerDto.LastName,Phone=registerDto.Phone};
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return new UserRegusterDto
            {
                Success = false,
                Errors = result.Errors?.Select(e => e.Description)

            };
        }

      //  var rolerName = (string.IsNullOrEmpty(registerDto.Role)? "User":registerDto.Role);
        var rolerName = string.IsNullOrEmpty(registerDto.Role) ? "User" : registerDto.Role;
        var roleResult = await _userManager.AddToRoleAsync(user, rolerName);

        if (!roleResult.Succeeded)
        {
            return new UserRegusterDto
            {
                Success = false,
                Errors = roleResult.Errors?.Select(e => e.Description)
                                      
            };
        }
         

        return new UserRegusterDto
        {
            Success = result.Succeeded,
            Errors = result.Errors?.Select(e => e.Description)
        };
    }

    public async Task<UserRegusterDto> LoginAsync(LoginDtos loginDto)
    {
        try
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
        catch (Exception ex)
        {
            // سجّل أو أعد رمي الخطأ مع التفاصيل
            throw new Exception($"Login error: {ex.Message}", ex);
        }
        
    }

    public async Task LogoutAsenc()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<UserDto> GetUserByIdAsync(string userId)
    {

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return null;
        var role =_userManager.GetRolesAsync(user);
  
        return new UserDto
        {
            Id = Guid.Parse(user.Id),
            Email=user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Role = role.Result.FirstOrDefault(),
        };
    }

    public async Task<UserDto> GetUserByEmailAsync(string Eamil)
    {
        var user = await _userManager.FindByEmailAsync(Eamil);
        if (user == null) return null;
        var role = _userManager.GetRolesAsync(user);

        return new UserDto
        {
            Id = Guid.Parse(user.Id),
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Role = role.Result.FirstOrDefault(),
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
        var userId = _IHttpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(userId);
    }

   

}
