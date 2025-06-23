using Microsoft.EntityFrameworkCore;

namespace trinetra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Module> Modules => Set<Module>();
        public DbSet<SubModule> SubModules => Set<SubModule>();
        public DbSet<TaskType> TaskTypes => Set<TaskType>();
        public DbSet<Status> Statuses => Set<Status>();
        public DbSet<User> Users => Set<User>();
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>().Property(t => t.TaskItemId).HasColumnName("TaskId");
            base.OnModelCreating(modelBuilder);
        }
    }
}
