using System;
using Microsoft.EntityFrameworkCore;

namespace trinetra.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext db)
        {
                    db.Database.EnsureCreated();

            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new User { Name = "Admin" },
                    new User { Name = "Demo" }
                );
            }
            if (!db.Clients.Any())
            {
                var client = new Client { Name = "Contoso" };
                var project = new Project { Name = "Project X", Client = client };
                var module = new Module { Name = "Module A", Project = project };
                var sub = new SubModule { Name = "Feature 1", Module = module };
                var tt = new TaskType { Name = "Bug" };
                var status = new Status { Name = "Open" };

                db.AddRange(client, project, module, sub, tt, status);
                db.SaveChanges();

                db.Tasks.Add(new TaskItem
                {
                    Title = "Sample task",
                    Description = "Demo",
                    DueDate = DateTime.Today.AddDays(1),
                    EstimatedEffortHr = 2,
                    AssignedToId = db.Users.First().UserId,
                    ClientId = client.ClientId,
                    ProjectId = project.ProjectId,
                    ModuleId = module.ModuleId,
                    SubModuleId = sub.SubModuleId,
                    TaskTypeId = tt.TaskTypeId,
                    StatusId = status.StatusId,
                    CreatedUtc = DateTime.UtcNow,
                    UpdatedUtc = DateTime.UtcNow
                });
            }
            db.SaveChanges();
        }
    }
}
    