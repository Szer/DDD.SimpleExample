using System.Data.Entity;
using System.Threading.Tasks;
using DDD.SimpleExample.ReadSide.Models;

namespace DDD.SimpleExample.ReadSide.Interfaces
{
    public interface IModelUpdater
    {
        DbSet<CustomerModel> Customers { get; }
        DbSet<ProjectModel> Projects { get; }
        Task<int> SaveChangesAsync();
    }
}