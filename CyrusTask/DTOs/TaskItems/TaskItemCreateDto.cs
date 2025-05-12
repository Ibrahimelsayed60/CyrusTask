using System.ComponentModel.DataAnnotations;

namespace CyrusTask.DTOs.TaskItems
{
    public class TaskItemCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ProjectId must be greater than 0.")]
        public int ProjectId { get; set; }

        public int? UserId { get; set; }
    }
}
