using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using trinetra.Data;

namespace trinetra.Pages
{
    [Authorize]
    public class MyTasksModel : PageModel
    {
        private readonly AppDbContext _db;
        public MyTasksModel(AppDbContext db) => _db = db;

        [BindProperty(SupportsGet = true)] public DateTime Date { get; set; } = DateTime.Today;
        public List<TaskItem> Tasks { get; set; } = new();
        public List<Status> Statuses { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            Tasks = await _db.Tasks.Include(t=>t.Status).Where(t=>t.AssignedToId==userId && t.DueDate.Date==Date.Date).ToListAsync();
            Statuses = await _db.Statuses.ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, int statusId)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task==null) return NotFound();
            task.StatusId = statusId;
            await _db.SaveChangesAsync();
            return new JsonResult(true);
        }
    }
}
