using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using trinetra.Data;

namespace trinetra.Pages
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private readonly AppDbContext _db;
        public DashboardModel(AppDbContext db) => _db = db;

        public IList<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public KpiModel KpiTotal = new("Total",0);
        public KpiModel KpiPending = new("Pending",0);
        public KpiModel KpiOverdue = new("Overdue",0);
        public KpiModel KpiEffort = new("Effort (h)",0);

        public class KpiModel(string title, decimal val)
        {
            public string Title { get; set; } = title;
            public decimal Value { get; set; } = val;
        }

        public async Task OnGetAsync()
        {
            Tasks = await _db.Tasks.Include(t=>t.Status).ToListAsync();
            KpiTotal.Value = Tasks.Count;
            KpiPending.Value = Tasks.Count(t=>t.Status!.Name=="Open");
            KpiOverdue.Value = Tasks.Count(t=>t.DueDate<DateTime.Today);
            KpiEffort.Value = Tasks.Sum(t=>t.EstimatedEffortHr);
        }
    }
}
