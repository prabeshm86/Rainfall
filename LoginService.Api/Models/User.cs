using System;
namespace LoginService.Api.Models
{
    public class User
    {
        public User()
        {
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }

    public enum UserRole
    {
        NORMAL, ADMIN
    }
}
