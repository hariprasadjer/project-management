namespace IssueTracker.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (!db.AppUsers.Any())
        {
            db.AppUsers.AddRange(
                new AppUser { Name = "admin" },
                new AppUser { Name = "dev" }
            );
        }
        if (!db.Clients.Any())
        {
            var client = new Client { Name = "DemoClient" };
            var project = new Project { Name = "DemoProject", Client = client };
            var module = new Module { Name = "Core", Project = project };
            var sub = new SubModule { Name = "Feature", Module = module };
            var type = new Type { Name = "Bug" };
            var status = new Status { Name = "Open" };
            db.AddRange(client, project, module, sub, type, status);
            db.SaveChanges();
            db.IssueTasks.Add(new IssueTask
            {
                IssueNo = "ISS-1",
                ModuleId = module.ModuleId,
                SubModuleId = sub.SubModuleId,
                FormPageName = "Index",
                IssueTitle = "Sample Issue",
                Description = "Demo description",
                StatusId = status.StatusId,
                TypeId = type.TypeId,
                AssignedToId = db.AppUsers.First().AppUserId,
                EffortHr = 1,
                CreatedUtc = DateTime.UtcNow
            });
        }
        db.SaveChanges();
    }
}
