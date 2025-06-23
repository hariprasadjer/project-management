using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;

[Authorize]
public class MyTasksModel : PageModel
{
    private readonly AppDbContext _db;
    public MyTasksModel(AppDbContext db) => _db = db;

    [BindProperty(SupportsGet = true)] public DateTime Date { get; set; } = DateTime.Today;
    public List<IssueTask> Tasks { get; set; } = new();
    public List<Status> Statuses { get; set; } = new();

    public async Task OnGetAsync()
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        Tasks = await _db.IssueTasks.Include(t=>t.Status)
            .Where(t=>t.AssignedToId==userId && t.CreatedUtc.Date==Date.Date)
            .ToListAsync();
        Statuses = await _db.Statuses.ToListAsync();
    }
}
