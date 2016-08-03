using System;
using System.Data.Entity;
using System.Threading.Tasks;
using DDD.SimpleExample.ReadSide.Models;

namespace DDD.SimpleExample.ReadSide.Interfaces
{
    public interface IModelUpdater : IDisposable
    {
        DbSet<CustomerModel> Customers { get; }
        DbSet<ProjectModel> Projects { get; }
        DbSet<UserModel> Users { get; }
        Task<int> SaveChangesAsync();
    }
}