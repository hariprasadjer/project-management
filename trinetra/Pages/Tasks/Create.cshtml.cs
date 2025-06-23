using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using trinetra.Data;

namespace trinetra.Pages.Tasks
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        public CreateModel(AppDbContext db) => _db = db;

        [BindProperty]
        public TaskItem Task { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            Task.CreatedUtc = Task.UpdatedUtc = DateTime.UtcNow;
            _db.Tasks.Add(Task);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
