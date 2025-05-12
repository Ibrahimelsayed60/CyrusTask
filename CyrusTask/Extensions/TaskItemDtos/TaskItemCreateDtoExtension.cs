using CyrusTask.DTOs.TaskItems;
using CyrusTask.Models;
using TaskStatusEnum = CyrusTask.Models.TaskStatus;

namespace CyrusTask.Extensions.TaskItemDtos
{
    public static class TaskItemCreateDtoExtension
    {

        public static TaskItem ToModel(this TaskItemCreateDto taskItemCreateDto)
        {
            return new TaskItem
            {
                Title = taskItemCreateDto.Title,
                Description = taskItemCreateDto.Description,
                Status = (TaskStatusEnum)Enum.Parse(typeof(TaskStatusEnum), taskItemCreateDto.Status),
                CreatedAt = taskItemCreateDto.CreatedAt,
                ProjectId = taskItemCreateDto.ProjectId,
                AssignedUserId = taskItemCreateDto?.UserId
                
            };
        }

    }
}
