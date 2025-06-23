namespace IssueTracker.Services;

public record AppUserCred(int Id, string Username, string Password);

public class UserService
{
    private readonly List<AppUserCred> _users = new()
    {
        new AppUserCred(1, "admin", "password"),
        new AppUserCred(2, "dev", "password")
    };

    public AppUserCred? Validate(string username, string password)
        => _users.FirstOrDefault(u => u.Username == username && u.Password == password);
}
