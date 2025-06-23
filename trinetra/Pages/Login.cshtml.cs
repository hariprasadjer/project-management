using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using trinetra.Services;

namespace trinetra.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _users;
        public LoginModel(UserService users) => _users = users;

        [BindProperty]
        public Credential Input { get; set; } = new();

        public record Credential
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _users.Validate(Input.Username, Input.Password);
            if (user == null) return Page();
            var claims = new[] { new Claim(ClaimTypes.Name, user.Username), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return RedirectToPage("/Dashboard");
        }
    }
}
