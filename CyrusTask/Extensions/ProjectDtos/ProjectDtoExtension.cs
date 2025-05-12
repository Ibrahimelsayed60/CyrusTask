using CyrusTask.DTOs.Projects;
using CyrusTask.DTOs.TaskItems;
using CyrusTask.Extensions.TaskItemDtos;
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
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Tasks = (List<TaskItemDto>)project.Tasks?.AsQueryable().ToDtos(),
            };
        }

        public static IEnumerable<ProjectDto> ToDtos(this IQueryable<Project> projects)
        {
            return projects.Select(project => ToDto(project)).ToList();
        }

        public static Project ToModel(this ProjectDto projectDto)
        {
            return new Project
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                Description = projectDto.Description,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,

            };
        }

    }
}
