using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trinetra.Data;

namespace trinetra.Pages.Masters
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;
        [BindProperty(SupportsGet = true)] public string Type { get; set; } = string.Empty;
        [BindProperty] public string NewItem { get; set; } = string.Empty;
        public List<string> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Items = await GetIQueryable()
                .Select(e => EF.Property<string>(e, "Name"))
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dbSet = GetDbSet();
            var entityType = dbSet.EntityType;
            var entity = Activator.CreateInstance(entityType.ClrType)!;
            entityType.FindProperty("Name")!.PropertyInfo!.SetValue(entity, NewItem);
            dbSet.Add(entity);
            await _db.SaveChangesAsync();
            return RedirectToPage(new { type = Type });
        }

        private IQueryable<object> GetIQueryable()
        {
            return Type.ToLower() switch
            {
                "client" => _db.Clients.Cast<object>(),
                "project" => _db.Projects.Cast<object>(),
                "module" => _db.Modules.Cast<object>(),
                "submodule" => _db.SubModules.Cast<object>(),
                "tasktype" => _db.TaskTypes.Cast<object>(),
                "status" => _db.Statuses.Cast<object>(),
                "user" => _db.Users.Cast<object>(),
                _ => _db.Clients.Cast<object>()
            };
        }

        private Microsoft.EntityFrameworkCore.Metadata.IEntityType GetEntityType()
        {
            return Type.ToLower() switch
            {
                "client" => _db.Model.FindEntityType(typeof(Client)),
                "project" => _db.Model.FindEntityType(typeof(Project)),
                "module" => _db.Model.FindEntityType(typeof(Module)),
                "submodule" => _db.Model.FindEntityType(typeof(SubModule)),
                "tasktype" => _db.Model.FindEntityType(typeof(TaskType)),
                "status" => _db.Model.FindEntityType(typeof(Status)),
                "user" => _db.Model.FindEntityType(typeof(User)),
                _ => _db.Model.FindEntityType(typeof(Client))
            };
        }

        private dynamic GetDbSet()
        {
            return Type.ToLower() switch
            {
                "client" => _db.Clients,
                "project" => _db.Projects,
                "module" => _db.Modules,
                "submodule" => _db.SubModules,
                "tasktype" => _db.TaskTypes,
                "status" => _db.Statuses,
                "user" => _db.Users,
                _ => _db.Clients
            };
        }
    }
}
