using CyrusTask.DTOs.TaskItems;
using CyrusTask.Extensions.TaskItemDtos;
using CyrusTask.Models;
using CyrusTask.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyrusTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IGenericRepository<TaskItem> _tasksRepo;

        public TasksController(IGenericRepository<TaskItem> tasksRepo)
        {
            _tasksRepo = tasksRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = (await _tasksRepo.GetAllAsync()).ToDtos();

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItemCreateDto taskCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _tasksRepo.AddAsync(taskCreateDto.ToModel());

            await _tasksRepo.SaveChangesAsync();

            return Ok(task);
        }

        //[HttpPut("{id:int}/assign")]
        //public Task<IActionResult> AssignTaskToUser(int id)
        //{

        //}

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, TaskItemCreateDto itemCreateDto)
        {
            var taskUpdated = itemCreateDto.ToModel();
            taskUpdated.Id = id;

            _tasksRepo.Update(taskUpdated);
            await _tasksRepo.SaveChangesAsync();
            return Ok(taskUpdated.ToDTO());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task= await _tasksRepo.GetByIdAsync(id);

            _tasksRepo.Delete(task);
            await _tasksRepo.SaveChangesAsync();
            return Ok();

        }

    }
}
