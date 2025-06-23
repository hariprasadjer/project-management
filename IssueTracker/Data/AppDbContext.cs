using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Module> Modules => Set<Module>();
    public DbSet<SubModule> SubModules => Set<SubModule>();
    public DbSet<Type> Types => Set<Type>();
    public DbSet<Status> Statuses => Set<Status>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<IssueTask> IssueTasks => Set<IssueTask>();
}
