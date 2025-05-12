namespace CyrusTask.Specifications.TaskSpecs
{
    public class TaskPaginationParams
    {
        private const int MaxPageSize = 10;

        public string? Sort { get; set; }

        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;
    }
}
