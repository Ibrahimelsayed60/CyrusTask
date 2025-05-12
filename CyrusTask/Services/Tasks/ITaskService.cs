using CyrusTask.DTOs.Projects;
using CyrusTask.DTOs.TaskItems;
using CyrusTask.Models;
using CyrusTask.Specifications.ProjectSpecs;
using CyrusTask.Specifications.TaskSpecs;

namespace CyrusTask.Services.Tasks
{
    public interface ITaskService
    {

        Task<IEnumerable<TaskItemDto>> GetAllTasks();

        Task<IEnumerable<TaskItem>> GetTasksForSpecificProject(int projectId);

        Task<TaskItem?> GetProjectById(int id);

        Task<TaskItemDto> CreateTask(TaskItemCreateDto taskCreateDto);

        Task<TaskItemDto> UpdateTaskStatus(int id, TaskItemCreateDto itemCreateDto);

        Task<TaskItemDto> AssignTaskToUser(int TaskId, int UserId);

        Task<IEnumerable<TaskItemDto>> FilterTasksByProjectAndUser(TaskSpecParams spec);

        Task<int> GetCountAsync(TaskSpecParams specParams);

        Task<bool> DeleteTask(TaskItem taskItem);

        Task<bool> DeleteHardTask(TaskItem taskItem);

        bool isExist(int id);

    }
}
