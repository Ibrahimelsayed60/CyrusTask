using CyrusTask.DTOs.Projects;
using CyrusTask.Extensions.ProjectDtos;
using CyrusTask.Helpers;
using CyrusTask.Models;
using CyrusTask.Repositories;
using CyrusTask.Services.Projects;
using CyrusTask.Specifications.ProjectSpecs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CyrusTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects([FromQuery] ProjectSpecParams specParams)
        {
            var projects= await _projectService.GetAllProject(specParams);

            var count = await _projectService.GetCountAsync(specParams);

            return Ok(new Pagination<ProjectDto>(specParams.PageIndex, specParams.PageSize, count, projects));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectById(id);

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectCreateDto projectCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            

            var projectCreated =await _projectService.CreateProject(projectCreateDto);
            return Ok(projectCreated);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectCreateDto projectCreateDto)
        {
            bool isExisted = _projectService.isExist(id);
            if (!isExisted)
            {
                return BadRequest("Project no found");
            }

            var project = await _projectService.UpdateProject(id, projectCreateDto);

            return Ok(project);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectService.GetProjectById(id);

            bool isDeleted = await _projectService.DeleteProject(project);

            return Ok(new
            {
                isDeleted = isDeleted
            });
        }

    }
}
