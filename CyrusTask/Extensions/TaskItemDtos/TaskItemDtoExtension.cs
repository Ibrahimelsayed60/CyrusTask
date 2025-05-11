using CyrusTask.DTOs.TaskItems;
using CyrusTask.Models;

namespace CyrusTask.Extensions.TaskItemDtos
{
    public static class TaskItemDtoExtension
    {

        public static TaskItemDto ToDTO(this TaskItem taskItem)
        {
            return new TaskItemDto
            {
                Title = taskItem.Title,
                Description = taskItem.Description,
                Status = taskItem.Status.ToString(),
                CreatedAt = taskItem.CreatedAt,
                ProjectId = taskItem.ProjectId
            };
        }

        public static IEnumerable<TaskItemDto> ToDtos(this IQueryable<TaskItem> taskItems)
        {
            return taskItems.Select(taskItem => ToDTO(taskItem)).ToList();
        }

    }
}
