using CyrusTask.DTOs.Users;
using CyrusTask.Models;
using Microsoft.AspNetCore.Identity;

namespace CyrusTask.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();

        Task<UserDto> CreateUserAsync(RegisterDto registerDTO);
        Task UpdateUserAsync(UserDto userDTO);
        Task<UserDto?> FindUserByEmailAsync(string email);

        Task<bool> CheckUserPasswordAsync(UserDto user, string password);

        string CreateToken(User user);

    }
}
