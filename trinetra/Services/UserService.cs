using System.Collections.Generic;
using System.Security.Claims;

namespace trinetra.Services
{
    public record AppUser(int Id, string Username, string Password);

    public class UserService
    {
        private readonly List<AppUser> _users = new()
        {
            new AppUser(1, "admin", "password"),
            new AppUser(2, "user", "password")
        };

        public AppUser? Validate(string username, string password)
            => _users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }
}
