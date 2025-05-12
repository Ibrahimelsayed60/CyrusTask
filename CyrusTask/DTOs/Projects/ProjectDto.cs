﻿using CyrusTask.DTOs.TaskItems;
using CyrusTask.Models;

namespace CyrusTask.DTOs.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<TaskItemDto>? Tasks { get; set; }

    }
}
