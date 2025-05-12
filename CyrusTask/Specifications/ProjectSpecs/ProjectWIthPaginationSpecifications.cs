using CyrusTask.Models;

namespace CyrusTask.Specifications.ProjectSpecs
{
    public class ProjectWIthPaginationSpecifications : BaseSpecifications<Project>
    {

        public ProjectWIthPaginationSpecifications(ProjectSpecParams specParams):base()
        {
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "StartDateDesc":
                        AddOrderByDesc(P => P.StartDate);
                        break;
                    case "StartDateAsc":
                        AddOrderBy(P => P.StartDate);
                        break;

                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }

    }
}
