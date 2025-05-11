using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CyrusTask.Models
{
    public class ProjectManagementContext : DbContext
    {

        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options): base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
