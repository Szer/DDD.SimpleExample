using DDD.SimpleExample.ReadSide.Helpers;
using DDD.SimpleExample.ReadSide.Models;

namespace DDD.SimpleExample.ReadSide.Mappings
{
    internal class UserModelConfiguration : BaseModelConfiguration<UserModel>
    {
        protected UserModelConfiguration()
        {
            HasMany(u => u.AssignedProjects)
                .WithMany(c => c.AssignedUsers);
        }
    }
}