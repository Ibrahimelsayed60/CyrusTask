namespace CyrusTask.Models
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; } // Enum: Pending, InProgress, Done
        public DateTime CreatedAt { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int? AssignedUserId { get; set; }
        public User AssignedUser { get; set; }
    }
}
