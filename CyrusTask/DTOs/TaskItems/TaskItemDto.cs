namespace CyrusTask.DTOs.TaskItems
{
    public class TaskItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } 
        public DateTime CreatedAt { get; set; }
        public int ProjectId { get; set; }

    }
}
