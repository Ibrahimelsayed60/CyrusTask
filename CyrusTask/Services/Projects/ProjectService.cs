using CyrusTask.DTOs.Projects;
using CyrusTask.Extensions.ProjectDtos;
using CyrusTask.Models;
using CyrusTask.Repositories;

namespace CyrusTask.Services.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<Project> _projectRepo;

        public ProjectService(IGenericRepository<Project> projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProject()
        {
            return (await _projectRepo.GetAllAsync()).ToDtos();
        }

        public async Task<ProjectDto> GetProjectById(int id)
        {
            return (await _projectRepo.GetByIdAsync(id)).ToDto();
        }

        public async Task<ProjectDto> CreateProject(ProjectCreateDto project)
        {
            var projectCreated = await _projectRepo.AddAsync(project.ToModel());
            await _projectRepo.SaveChangesAsync();

            return projectCreated.ToDto();
        }

        public async Task<ProjectDto> UpdateProject(int id, ProjectCreateDto projectCreateDto)
        {
            var project = projectCreateDto.ToModel();
            project.Id = id;

            _projectRepo.Update(project);

            await _projectRepo.SaveChangesAsync();

            return project.ToDto();
        }

        public async Task<bool> DeleteProject(ProjectDto project)
        {
            _projectRepo.Delete(project.ToModel());

            return await _projectRepo.SaveChangesAsync() > 0;
        }

        public bool isExist(int id)
        {
            return _projectRepo.Exists(id);
        }
    }
}
