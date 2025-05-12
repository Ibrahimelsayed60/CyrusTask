using CyrusTask.DTOs.Projects;
using CyrusTask.Specifications.ProjectSpecs;

namespace CyrusTask.Services.Projects
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProject(ProjectSpecParams projectSpecs);

        Task<int> GetCountAsync(ProjectSpecParams specParams);

        Task<ProjectDto?> GetProjectById(int id);

        Task<ProjectDto> CreateProject(ProjectCreateDto project);

        Task<ProjectDto> UpdateProject(int id, ProjectCreateDto projectCreateDto);

        Task<bool> DeleteProject(ProjectDto projectDto);

        bool isExist(int id);
    }
}
