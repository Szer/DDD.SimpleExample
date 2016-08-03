using System;
using System.Linq;
using DDD.SimpleExample.ReadSide.Models;

namespace DDD.SimpleExample.ReadSide.Interfaces
{
    public interface IModelReader : IDisposable
    {
        IQueryable<CustomerModel> Customers { get; }
        IQueryable<ProjectModel> Projects { get; }
        IQueryable<UserModel> Users { get; }
    }
}