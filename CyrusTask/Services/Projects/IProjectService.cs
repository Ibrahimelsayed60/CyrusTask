using CyrusTask.DTOs.Projects;
using CyrusTask.Models;
using CyrusTask.Specifications.ProjectSpecs;

namespace CyrusTask.Services.Projects
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProject(ProjectSpecParams projectSpecs);

        Task<int> GetCountAsync(ProjectSpecParams specParams);

        Task<ProjectDto?> GetProjectById(int id);

        Task<Project?> GetProjectByIdTracked(int id);

        Task<ProjectDto> CreateProject(ProjectCreateDto project);

        Task<ProjectDto> UpdateProject(int id, ProjectCreateDto projectCreateDto);

        Task<bool> DeleteProject(Project projectDto);

        Task<bool> HardDeleteProject(Project project);

        bool isExist(int id);
    }
}
