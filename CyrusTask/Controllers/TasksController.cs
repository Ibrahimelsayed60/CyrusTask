using CyrusTask.DTOs.TaskItems;
using CyrusTask.Extensions.TaskItemDtos;
using CyrusTask.Models;
using CyrusTask.Repositories;
using CyrusTask.Services.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyrusTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IGenericRepository<TaskItem> _tasksRepo;
        private readonly ITaskService _taskService;

        public TasksController(IGenericRepository<TaskItem> tasksRepo, ITaskService taskService)
        {
            _tasksRepo = tasksRepo;
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllProject();

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

        //[Authorize]
        //[HttpPut("{id:int}/assign")]
        //public Task<IActionResult> AssignTaskToUser(int id)
        //{

        //}

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
