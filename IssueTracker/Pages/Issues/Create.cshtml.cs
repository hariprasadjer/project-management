using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;

[Authorize]
public class Issues_CreateModel : PageModel
{
    private readonly AppDbContext _db;
    public Issues_CreateModel(AppDbContext db) => _db = db;

    [BindProperty]
    public IssueTask Issue { get; set; } = new();
    public List<Module> Modules { get; set; } = new();
    public List<SubModule> SubModules { get; set; } = new();
    public List<Status> Statuses { get; set; } = new();
    public List<Type> Types { get; set; } = new();
    public List<AppUser> Users { get; set; } = new();

    public async Task OnGetAsync()
    {
        Modules = await _db.Modules.ToListAsync();
        SubModules = await _db.SubModules.ToListAsync();
        Statuses = await _db.Statuses.ToListAsync();
        Types = await _db.Types.ToListAsync();
        Users = await _db.AppUsers.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Issue.CreatedUtc = DateTime.UtcNow;
        _db.IssueTasks.Add(Issue);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
