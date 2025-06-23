using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IssueTracker.Data;

[Authorize]
public class Issues_DeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public Issues_DeleteModel(AppDbContext db) => _db = db;

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
        var issue = await _db.IssueTasks.FindAsync(Issue.IssueTaskId);
        if (issue != null)
        {
            _db.IssueTasks.Remove(issue);
            await _db.SaveChangesAsync();
        }
        return RedirectToPage("Index");
    }
}
