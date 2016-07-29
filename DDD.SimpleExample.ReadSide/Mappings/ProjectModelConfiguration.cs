using DDD.SimpleExample.ReadSide.Helpers;
using DDD.SimpleExample.ReadSide.Models;

namespace DDD.SimpleExample.ReadSide.Mappings
{
    internal class ProjectModelConfiguration : BaseModelConfiguration<ProjectModel>
    {
        protected ProjectModelConfiguration()
        {
            HasRequired(project => project.CustomerModel)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CustomerId);
        }
    }
}