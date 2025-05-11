using CyrusTask.DTOs.Projects;
using CyrusTask.Models;
using System.Runtime.CompilerServices;

namespace CyrusTask.Extensions.ProjectDtos
{
    public static class ProjectDtoExtension
    {

        public static ProjectDto ToDto (this Project project)
        {
            return new ProjectDto
            {
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Tasks = project.Tasks,
            };
        }

        public static IEnumerable<ProjectDto> ToDtos(this IQueryable<Project> projects)
        {
            return projects.Select(project => ToDto(project)).ToList();
        }

    }
}
