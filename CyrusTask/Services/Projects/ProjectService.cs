using CyrusTask.DTOs.Projects;
using CyrusTask.Extensions.ProjectDtos;
using CyrusTask.Models;
using CyrusTask.Repositories;
using CyrusTask.Services.Tasks;
using CyrusTask.Specifications.ProjectSpecs;

namespace CyrusTask.Services.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<Project> _projectRepo;
        private readonly ITaskService _taskService;

        public ProjectService(IGenericRepository<Project> projectRepo, ITaskService taskService)
        {
            _projectRepo = projectRepo;
            _taskService = taskService;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProject(ProjectSpecParams projectSpecs)
        {
            var spec = new ProjectWIthPaginationSpecifications(projectSpecs);

            return _projectRepo.GetAllWithSpec(spec).ToDtos();

            //return (await _projectRepo.GetAllAsync()).ToDtos();
        }

        public async Task<ProjectDto?> GetProjectById(int id)
        {
            var spec = new ProjectIncludeSpecifications(id);
            return (await _projectRepo.GetWithSpecAsync(spec))?.ToDto();
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

        public async Task<bool> DeleteProject(Project project)
        {

            var tasks = await _taskService.GetTasksForSpecificProject(project.Id);

            foreach(var task in tasks)
            {
                await _taskService.DeleteTask(task);
            }

            _projectRepo.Delete(project);

            return await _projectRepo.SaveChangesAsync() > 0;
        }

        public async Task<bool> HardDeleteProject(ProjectDto projectDto)
        {
            _projectRepo.HardDelete(projectDto.ToModel());

            return await _projectRepo.SaveChangesAsync() > 0;
        }

        public bool isExist(int id)
        {
            return _projectRepo.Exists(id);
        }

        public async Task<int> GetCountAsync(ProjectSpecParams specParams)
        {
            var spec = new ProjectForCountSpecification();
            return await  _projectRepo.GetCountAsync(spec);
        }

        public async Task<Project?> GetProjectByIdTracked(int id)
        {
            return await _projectRepo.GetByIdAsync(id);
        }

        public async Task<bool> HardDeleteProject(Project project)
        {
            _projectRepo.HardDelete(project);

            return await _projectRepo.SaveChangesAsync() > 0;
        }
    }
}
