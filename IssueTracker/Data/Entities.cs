using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTracker.Data;

public class Client
{
    public int ClientId { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}

public class Project
{
    public int ProjectId { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public ICollection<Module> Modules { get; set; } = new List<Module>();
}

public class Module
{
    public int ModuleId { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public ICollection<SubModule> SubModules { get; set; } = new List<SubModule>();
}

public class SubModule
{
    public int SubModuleId { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
    public int ModuleId { get; set; }
    public Module? Module { get; set; }
}

public class Type
{
    public int TypeId { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
}

public class Status
{
    public int StatusId { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
}

public class AppUser
{
    public int AppUserId { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
}

public class IssueTask
{
    public int IssueTaskId { get; set; }
    [Required, MaxLength(30)] public string IssueNo { get; set; } = string.Empty;
    public int ModuleId { get; set; }
    public int SubModuleId { get; set; }
    [Required, MaxLength(100)] public string FormPageName { get; set; } = string.Empty;
    [Required, MaxLength(150)] public string IssueTitle { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public int TypeId { get; set; }
    [Column(TypeName="decimal(5,1)")] public decimal? EffortHr { get; set; }
    [MaxLength(400)] public string? RemarkInternal { get; set; }
    [MaxLength(400)] public string? RemarkClient { get; set; }
    public int AssignedToId { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedUtc { get; set; }

    public Module? Module { get; set; }
    public SubModule? SubModule { get; set; }
    public Status? Status { get; set; }
    public Type? Type { get; set; }
    public AppUser? AssignedTo { get; set; }
}
