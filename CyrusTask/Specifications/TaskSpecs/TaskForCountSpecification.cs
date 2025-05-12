using CyrusTask.Models;

namespace CyrusTask.Specifications.TaskSpecs
{
    public class TaskForCountSpecification:BaseSpecifications<TaskItem>
    {
        public TaskForCountSpecification(TaskSpecParams taskSpec) :base(
            t => t.ProjectId == taskSpec.ProjectId && t.AssignedUserId == taskSpec.UserId
            )
        {
            
        }
    }
}
