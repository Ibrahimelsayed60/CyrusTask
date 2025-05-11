namespace CyrusTask.DTOs.Users
{
    public class UserDto
    {

        public string FullName { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string? Token { get; set; }

    }
}
