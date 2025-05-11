using CyrusTask.Models;

namespace CyrusTask.Specifications.ProjectSpecs
{
    public class ProjectIncludeSpecifications:BaseSpecifications<Project>
    {
        public ProjectIncludeSpecifications(int id):base(p => p.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Tasks);
        }
    }
}
