using CyrusTask.Models;
using System.ComponentModel.DataAnnotations;

namespace CyrusTask.DTOs.Projects
{
    public class ProjectCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
