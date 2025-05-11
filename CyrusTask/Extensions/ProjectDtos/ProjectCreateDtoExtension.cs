using CyrusTask.DTOs.Projects;
using CyrusTask.Models;

namespace CyrusTask.Extensions.ProjectDtos
{
    public static class ProjectCreateDtoExtension
    {
        public static Project ToModel(this ProjectCreateDto projectCreateDto)
        {
            return new Project
            {
                Name = projectCreateDto.Name,
                Description = projectCreateDto.Description,
                StartDate = projectCreateDto.StartDate,
                EndDate = projectCreateDto.EndDate,

            };
        }
    }
}
