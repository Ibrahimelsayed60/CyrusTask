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
        public async Task<ActionResult<Pagination<ProjectDto>>> GetAllProjects([FromQuery] ProjectSpecParams specParams)
        {

            // Validate specParams
            if (specParams.PageIndex <= 0)
                return BadRequest("Page index must be greater than 0.");
            

            try
            {
                var projects = await _projectService.GetAllProject(specParams);

                var count = await _projectService.GetCountAsync(specParams);

                return Ok(new Pagination<ProjectDto>(specParams.PageIndex, specParams.PageSize, count, projects));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the projects.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {

            if (id <= 0)
                return BadRequest("Invalid project ID.");

            try
            {
                var project = await _projectService.GetProjectById(id);

                if (project == null)
                    return NotFound("Project not found.");

                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the projects.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] ProjectCreateDto projectCreateDto)
        {

            if (projectCreateDto == null)
                return BadRequest("Project data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var projectCreated = await _projectService.CreateProject(projectCreateDto);

                if (projectCreated == null)
                    return StatusCode(500, "Failed to create project.");

                return Ok(projectCreated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while Creating the projects.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProjectDto>> UpdateProject(int id, [FromBody] ProjectCreateDto projectCreateDto)
        {
            if (id <= 0)
                return BadRequest("Invalid project ID.");

            if (projectCreateDto == null)
                return BadRequest("Project data is required.");

            try
            {
                bool isExisted = _projectService.isExist(id);
                if (!isExisted)
                {
                    return NotFound("Project no found");
                }

                var updatedProject = await _projectService.UpdateProject(id, projectCreateDto);

                if (updatedProject == null)
                    return StatusCode(500, "Failed to update the project.");

                return Ok(updatedProject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the project.");
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {

            if (id <= 0)
                return BadRequest("Invalid project ID.");

            try
            {
                var project = await _projectService.GetProjectByIdTracked(id);

                if (project == null)
                    return NotFound("Project not found.");

                bool isDeleted = await _projectService.DeleteProject(project);

                if (!isDeleted)
                    return StatusCode(500, "Failed to delete the project.");

                return Ok(new
                {
                    isDeleted = isDeleted
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:int}/hard")]
        public async Task<IActionResult> DeleteHardProject(int id)
        {

            if (id <= 0)
                return BadRequest("Invalid project ID.");

            try
            {
                var project = (await _projectService.GetProjectByIdTracked(id));

                if (project == null)
                    return NotFound("Project not found.");

                bool isDeleted = await _projectService.HardDeleteProject(project);

                if (!isDeleted)
                    return StatusCode(500, "Failed to delete the project.");

                return Ok(new
                {
                    isDeleted = isDeleted
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
