using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trinetra.Data
{
    public class Client { public int ClientId { get; set; } [Required, MaxLength(100)] public string Name { get; set; } = string.Empty; }
    public class Project { public int ProjectId { get; set; } [Required, MaxLength(100)] public string Name { get; set; } = string.Empty; public int ClientId { get; set; } public Client? Client { get; set; } }
    public class Module { public int ModuleId { get; set; } [Required, MaxLength(100)] public string Name { get; set; } = string.Empty; public int ProjectId { get; set; } public Project? Project { get; set; } }
    public class SubModule { public int SubModuleId { get; set; } [Required, MaxLength(100)] public string Name { get; set; } = string.Empty; public int ModuleId { get; set; } public Module? Module { get; set; } }
    public class TaskType { public int TaskTypeId { get; set; } [Required, MaxLength(100)] public string Name { get; set; } = string.Empty; }
    public class Status { public int StatusId { get; set; } [Required, MaxLength(100)] public string Name { get; set; } = string.Empty; }
    public class User { public int UserId { get; set; } [Required, MaxLength(100)] public string Name { get; set; } = string.Empty; }

    [Table("Task")]
    public class TaskItem
    {
        public int TaskItemId { get; set; }
        [Required, MaxLength(150)] public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [DataType(DataType.Date)] public DateTime DueDate { get; set; }
        [Column(TypeName="decimal(5,1)")] public decimal EstimatedEffortHr { get; set; }
        public int AssignedToId { get; set; }
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public int ModuleId { get; set; }
        public int SubModuleId { get; set; }
        public int TaskTypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }

        public User? AssignedTo { get; set; }
        public Client? Client { get; set; }
        public Project? Project { get; set; }
        public Module? Module { get; set; }
        public SubModule? SubModule { get; set; }
        public TaskType? TaskType { get; set; }
        public Status? Status { get; set; }
    }
}
