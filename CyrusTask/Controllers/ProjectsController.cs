using CyrusTask.DTOs.Projects;
using CyrusTask.Extensions.ProjectDtos;
using CyrusTask.Models;
using CyrusTask.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyrusTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IGenericRepository<Project> _projectRepo;

        public ProjectsController(IGenericRepository<Project> projectRepo)
        {
            _projectRepo = projectRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects= (await _projectRepo.GetAllAsync()).ToDtos();

            return Ok(projects);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = (await _projectRepo.GetByIdAsync(id)).ToDto();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectCreateDto projectCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            

            var projectCreated =await _projectRepo.AddAsync(projectCreateDto.ToModel());
            await _projectRepo.SaveChangesAsync();
            return Ok(projectCreated);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectCreateDto projectCreateDto)
        {
            bool isExisted = _projectRepo.Exists(id);
            if (!isExisted)
            {
                return BadRequest("Project no found");
            }

            var project = projectCreateDto.ToModel();
            project.Id = id ;

            _projectRepo.Update(project);

            await _projectRepo.SaveChangesAsync();

            return Ok(project);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepo.GetByIdAsync(id);

            _projectRepo.Delete(project);

            await _projectRepo.SaveChangesAsync();

            return Ok();
        }

    }
}
