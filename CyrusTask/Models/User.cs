namespace CyrusTask.Models
{
    public class User:BaseEntity
    {

        public string FullName { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }

    }
}
