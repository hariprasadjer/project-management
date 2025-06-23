using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;

[Authorize]
public class Issues_IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public Issues_IndexModel(AppDbContext db) => _db = db;

    public IList<IssueTask> Issues { get; set; } = new List<IssueTask>();

    public async Task OnGetAsync()
    {
        Issues = await _db.IssueTasks.Include(i=>i.Status).ToListAsync();
        ViewBag.Clients = await _db.Clients.ToListAsync();
        ViewBag.Projects = await _db.Projects.ToListAsync();
        ViewBag.Modules = await _db.Modules.ToListAsync();
        ViewBag.SubModules = await _db.SubModules.ToListAsync();
    }
}
