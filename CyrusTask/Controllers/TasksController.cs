using CyrusTask.DTOs.Projects;
using CyrusTask.DTOs.TaskItems;
using CyrusTask.Extensions;
using CyrusTask.Extensions.TaskItemDtos;
using CyrusTask.Helpers;
using CyrusTask.Models;
using CyrusTask.Repositories;
using CyrusTask.Services.Tasks;
using CyrusTask.Services.Users;
using CyrusTask.Specifications.TaskSpecs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyrusTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public TasksController(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllTasks()
        //{
        //    try
        //    {
        //        var tasks = await _taskService.GetAllTasks();

        //        if (tasks == null || !tasks.Any())
        //            return NoContent();

        //        return Ok(tasks);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while retrieving tasks.");
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> GetTaskInProjectAssignedToUser([FromQuery]TaskSpecParams spec)
        {
            if (spec == null)
            {
                return BadRequest("Filter parameters are required.");
            }

            if (spec.PageIndex <= 0)
                return BadRequest("Page index must be greater than 0.");

            try
            {
                var tasks = await _taskService.FilterTasksByProjectAndUser(spec);

                if (tasks == null || !tasks.Any())
                    return NoContent();

                var count = await _taskService.GetCountAsync(spec);

                return Ok(new Pagination<TaskItemDto>(spec.PageIndex, spec.PageSize, count, tasks));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while filtering tasks.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItemCreateDto taskCreateDto)
        {
            if (taskCreateDto == null)
            {
                return BadRequest("Task data is required.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Enum.TryParse(typeof(Models.TaskStatus), taskCreateDto.Status, ignoreCase: true, out var status))
            {
                return BadRequest($"Invalid status. Allowed values are: {string.Join(", ", Enum.GetNames(typeof(Models.TaskStatus)))}");
            }

            try
            {
                var task = await _taskService.CreateTask(taskCreateDto);

                if (task == null)
                    return StatusCode(500, "Failed to create the task.");

                return Ok(task);
            } 
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the task.");
            }
        }

        [Authorize]
        [HttpPut("{id:int}/assign")]
        public async Task<IActionResult> AssignTaskToUser(int id)
        {
            try
            {
                var email = User.GetEmail();

                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("Invalid or missing user email.");
                }

                var userDto = await _userService.FindUserByEmailAsync(email);

                if (userDto == null)
                {
                    return NotFound("User not found.");
                }

                var task = await _taskService.AssignTaskToUser(id, userDto.Id);

                if (task == null)
                {
                    return NotFound("Task not found or cannot be assigned.");
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while assigning the task.");
            }
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] TaskItemCreateDto itemCreateDto)
        {
            if (itemCreateDto == null)
            {
                return BadRequest("Task data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool isExist = _taskService.isExist(id);

                if (!isExist)
                {
                    return BadRequest("This Task not found");
                }

                if (!Enum.TryParse(typeof(Models.TaskStatus), itemCreateDto.Status, ignoreCase: true, out var status))
                {
                    return BadRequest($"Invalid status. Allowed values are: {string.Join(", ", Enum.GetNames(typeof(Models.TaskStatus)))}");
                }

                var taskUpdatedDto = await _taskService.UpdateTaskStatus(id, itemCreateDto);

                if (taskUpdatedDto == null)
                {
                    return StatusCode(500, "Failed to update task status.");
                }

                return Ok(taskUpdatedDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the task status.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var task = await _taskService.GetProjectById(id);

                if (task == null)
                {
                    return NotFound("Task not found.");
                }

                bool isDeleted = await _taskService.DeleteTask(task);

                if(!isDeleted)
                {
                    return StatusCode(500, "Failed to delete the task.");
                }

                return Ok("Task deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the task.");
            }

        }

    }
}
