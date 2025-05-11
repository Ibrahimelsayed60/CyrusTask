using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyrusTask.Models.config
{
    public class TaskItemCOnfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.Property(x => x.Status).HasConversion(
                o => o.ToString(),
                o => (TaskStatus)Enum.Parse(typeof(TaskStatus), o)
            );
        }
    }
}
