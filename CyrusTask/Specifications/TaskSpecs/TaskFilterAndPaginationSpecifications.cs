using CyrusTask.Models;

namespace CyrusTask.Specifications.TaskSpecs
{
    public class TaskFilterAndPaginationSpecifications: BaseSpecifications<TaskItem>
    {

        public TaskFilterAndPaginationSpecifications(TaskSpecParams taskSpec): base(
            t => t.ProjectId == taskSpec.ProjectId && t.AssignedUserId == taskSpec.UserId
            )
        {

            if (!string.IsNullOrEmpty(taskSpec.Sort))
            {
                switch (taskSpec.Sort)
                {
                    case "CreatedAtDesc":
                        AddOrderByDesc(P => P.CreatedAt);
                        break;
                    case "StatusAsc":
                        AddOrderBy(P => P.Status);
                        break;

                    default:
                        AddOrderBy(P => P.Title);
                        break;

                }
            }
            else
            {
                AddOrderBy(P => P.Title);
            }

            ApplyPagination((taskSpec.PageIndex - 1) * taskSpec.PageSize, taskSpec.PageSize);

        }

    }
}
