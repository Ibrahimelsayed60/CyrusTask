using CyrusTask.DTOs.TaskItems;
using CyrusTask.DTOs.Users;
using CyrusTask.Models;

namespace CyrusTask.Extensions.UserDtos
{
    public static class UserDtoExtension
    {

        public static User ToModel(this UserDto userDto)
        {
            return new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
            };
        }

        public static User ToRegisterModel(this RegisterDto registerDto)
        {
            return new User
            {
                Email = registerDto.Email,
                PasswordHash = registerDto.Password,
                FullName = registerDto.FullName,
            };
        }

        public static UserDto ToDTO(this User user)
        {
            return new UserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
            };
        }

        public static IEnumerable<UserDto> ToDtos(this IQueryable<User> taskItems)
        {
            return taskItems.Select(taskItem => ToDTO(taskItem)).ToList();
        }

    }
}
