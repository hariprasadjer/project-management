using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using trinetra.Data;

namespace trinetra.Pages.Tasks
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        public EditModel(AppDbContext db) => _db = db;

        [BindProperty]
        public TaskItem Task { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return RedirectToPage("Index");
            Task = task;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existing = await _db.Tasks.FindAsync(Task.TaskItemId);
            if (existing == null) return RedirectToPage("Index");
            existing.Title = Task.Title;
            existing.DueDate = Task.DueDate;
            existing.UpdatedUtc = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
