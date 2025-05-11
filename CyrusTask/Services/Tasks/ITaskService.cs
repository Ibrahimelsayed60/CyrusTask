using CyrusTask.DTOs.Projects;
using CyrusTask.DTOs.TaskItems;
using CyrusTask.Models;

namespace CyrusTask.Services.Tasks
{
    public interface ITaskService
    {

        Task<IEnumerable<TaskItemDto>> GetAllProject();

        Task<TaskItem?> GetProjectById(int id);

        Task<TaskItemDto> CreateTask(TaskItemCreateDto taskCreateDto);

        Task<TaskItemDto> UpdateTaskStatus(int id, TaskItemCreateDto itemCreateDto);

        Task<TaskItemDto> AssignTaskToUser(int TaskId, int UserId);

        Task<bool> DeleteTask(TaskItem taskItem);

        bool isExist(int id);

    }
}
