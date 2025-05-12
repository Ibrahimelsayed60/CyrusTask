using CyrusTask.DTOs.TaskItems;
using CyrusTask.Extensions;
using CyrusTask.Extensions.TaskItemDtos;
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

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllProject();

            return Ok(tasks);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetTaskInProjectAssignedToUser([FromQuery]TaskSpecParams spec)
        {
            var tasks = await _taskService.FilterTasksByProjectAndUser(spec);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItemCreateDto taskCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _taskService.CreateTask(taskCreateDto);

            return Ok(task);
        }

        [Authorize]
        [HttpPut("{id:int}/assign")]
        public async Task<IActionResult> AssignTaskToUser(int id)
        {
            var email = User.GetEmail();

            var userDto = await _userService.FindUserByEmailAsync(email);


            var task = await _taskService.AssignTaskToUser(id, userDto.Id);

            return Ok(task);
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, TaskItemCreateDto itemCreateDto)
        {
            bool isExist = _taskService.isExist(id);

            if (!isExist)
            {
                return BadRequest("This Task not found");
            }

            var taskUpdatedDto = await _taskService.UpdateTaskStatus(id, itemCreateDto);
            return Ok(taskUpdatedDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task= await _taskService.GetProjectById(id);

            if (task is null)
                return BadRequest("This task not found");

            await _taskService.DeleteTask(task);
            return Ok();

        }

    }
}
