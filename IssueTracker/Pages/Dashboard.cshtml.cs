using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;

[Authorize]
public class DashboardModel : PageModel
{
    private readonly AppDbContext _db;
    public DashboardModel(AppDbContext db) => _db = db;

    public IList<IssueTask> Issues { get; set; } = new List<IssueTask>();
    public (string Title, decimal Value) KpiTotal = ("Total",0);
    public (string Title, decimal Value) KpiPending = ("Pending",0);
    public (string Title, decimal Value) KpiOverdue = ("Overdue",0);
    public (string Title, decimal Value) KpiEffort = ("Effort (h)",0);

    public async Task OnGetAsync()
    {
        Issues = await _db.IssueTasks.Include(i=>i.Status).ToListAsync();
        KpiTotal.Value = Issues.Count;
        KpiPending.Value = Issues.Count(i=>i.Status!.Name=="Open");
        KpiOverdue.Value = Issues.Count(i=>i.UpdatedUtc.HasValue==false);
        KpiEffort.Value = Issues.Sum(i=>i.EffortHr??0);

        ViewBag.Clients = await _db.Clients.ToListAsync();
        ViewBag.Projects = await _db.Projects.ToListAsync();
        ViewBag.Modules = await _db.Modules.ToListAsync();
        ViewBag.SubModules = await _db.SubModules.ToListAsync();
    }
}
