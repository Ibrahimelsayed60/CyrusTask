using CyrusTask.DTOs.TaskItems;
using CyrusTask.Extensions.TaskItemDtos;
using CyrusTask.Models;
using CyrusTask.Repositories;
using CyrusTask.Specifications.TaskSpecs;
using System.Threading.Tasks;

namespace CyrusTask.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly IGenericRepository<TaskItem> _taskRepo;

        public TaskService(IGenericRepository<TaskItem> taskRepo)
        {
            _taskRepo = taskRepo;
        }

        

        public async Task<IEnumerable<TaskItemDto>> GetAllTasks()
        {
            var tasks = (await _taskRepo.GetAllAsync()).ToDtos();
            return tasks;
        }

        public async Task<TaskItemDto> CreateTask(TaskItemCreateDto taskCreateDto)
        {
            var task = await _taskRepo.AddAsync(taskCreateDto.ToModel());

            await _taskRepo.SaveChangesAsync();

            return task.ToDTO();
        }

        public async Task<TaskItemDto> UpdateTaskStatus(int id, string UpdatedStatus)
        {
            var taskUpdated = await _taskRepo.GetByIdAsync(id);

            taskUpdated.Status = (Models.TaskStatus)Enum.Parse(typeof(Models.TaskStatus), UpdatedStatus);

            _taskRepo.Update(taskUpdated);
            await _taskRepo.SaveChangesAsync();

            return taskUpdated.ToDTO();
        }

        public bool isExist(int id)
        {
            return _taskRepo.Exists(id);
        }

        public async Task<bool> DeleteTask(TaskItem taskItem)
        {
            _taskRepo.Delete(taskItem);
            return await _taskRepo.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteHardTask(TaskItem taskItem)
        {
            _taskRepo.HardDelete(taskItem);
            return await _taskRepo.SaveChangesAsync() > 0;
        }

        public async Task<TaskItem?> GetProjectById(int id)
        {
            return await _taskRepo.GetByIdAsync(id);
        }



        public async Task<TaskItemDto> AssignTaskToUser(int TaskId, int UserId)
        {
            var task = await _taskRepo.GetByIdAsync(TaskId);

            if(task.AssignedUserId is null)
            {
                task.AssignedUserId = UserId;
            }
            else
            {
                task.AssignedUserId = null;
            }

            _taskRepo.Update(task);

            await _taskRepo.SaveChangesAsync();

            return task.ToDTO();
        }

        public async Task<IEnumerable<TaskItemDto>> FilterTasksByProjectAndUser(TaskSpecParams spec)
        {
            var taskSpec = new TaskFilterAndPaginationSpecifications(spec);
            var tasks = _taskRepo.GetAllWithSpec(taskSpec);
            return tasks.ToDtos();
        }

        public async Task<int> GetCountAsync(TaskSpecParams specParams)
        {
            var spec = new TaskForCountSpecification(specParams);

            return await _taskRepo.GetCountAsync(spec);
        }

        public async Task<IEnumerable<TaskItem>> GetTasksForSpecificProject(int projectId)
        {
            return (await _taskRepo.GetAllAsync()).Where(t => t.ProjectId == projectId).ToList();
        }
    }
}
