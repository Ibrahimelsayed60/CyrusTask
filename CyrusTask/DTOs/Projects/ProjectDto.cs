using CyrusTask.Models;

namespace CyrusTask.DTOs.Projects
{
    public class ProjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<TaskItem>? Tasks { get; set; }

    }
}
