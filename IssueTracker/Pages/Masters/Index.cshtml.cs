using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;

[Authorize]
public class Masters_IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public Masters_IndexModel(AppDbContext db) => _db = db;

    [BindProperty(SupportsGet = true)] public string Type { get; set; } = string.Empty;
    [BindProperty] public string NewItem { get; set; } = string.Empty;
    public List<string> Items { get; set; } = new();

    public async Task OnGetAsync()
    {
        Items = await GetSet().Select(e => EF.Property<string>(e, "Name")).ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var set = GetSet();
        var entity = Activator.CreateInstance(set.EntityType.ClrType)!;
        set.EntityType.FindProperty("Name")!.PropertyInfo!.SetValue(entity, NewItem);
        set.Add(entity);
        await _db.SaveChangesAsync();
        return RedirectToPage(new { type = Type });
    }

    private DbSet<dynamic> GetSet() => Type.ToLower() switch
    {
        "client" => (DbSet<dynamic>)_db.Clients,
        "project" => _db.Projects,
        "module" => _db.Modules,
        "submodule" => _db.SubModules,
        "type" => _db.Types,
        "status" => _db.Statuses,
        "user" => _db.AppUsers,
        _ => _db.Clients
    };
}
