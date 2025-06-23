using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using trinetra.Data;

namespace trinetra.Pages.Tasks
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;
        public IList<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public async Task OnGetAsync()
        {
            Tasks = await _db.Tasks.Include(t=>t.Status).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task != null)
            {
                _db.Tasks.Remove(task);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
