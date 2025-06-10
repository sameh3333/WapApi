using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs;
namespace BL.Contracts
{
     public interface IUserService
    {

        Task<UserRegusterDto> RegisterAsync(UserDto register);
        Task<UserRegusterDto> LoginAsync(LoginDtos loginDto);
        Task LogoutAsenc();
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<UserDto> GetUserByEmailAsync(string Eamil);
        Task<IEnumerable<UserDto>> GetUserByIdAsync();
        Guid GetLoggedInServices();
    }
}
