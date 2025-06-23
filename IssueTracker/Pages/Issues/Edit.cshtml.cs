using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IssueTracker.Data;

[Authorize]
public class Issues_EditModel : PageModel
{
    private readonly AppDbContext _db;
    public Issues_EditModel(AppDbContext db) => _db = db;

    [BindProperty]
    public IssueTask Issue { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var issue = await _db.IssueTasks.FindAsync(id);
        if (issue == null) return RedirectToPage("Index");
        Issue = issue;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var existing = await _db.IssueTasks.FindAsync(Issue.IssueTaskId);
        if (existing == null) return RedirectToPage("Index");
        existing.IssueTitle = Issue.IssueTitle;
        existing.Description = Issue.Description;
        existing.UpdatedUtc = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
