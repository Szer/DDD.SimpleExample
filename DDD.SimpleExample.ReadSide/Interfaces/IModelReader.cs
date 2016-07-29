using System.Linq;
using DDD.SimpleExample.ReadSide.Models;

namespace DDD.SimpleExample.ReadSide.Interfaces
{
    public interface IModelReader
    {
        IQueryable<CustomerModel> Customers { get; }
        IQueryable<ProjectModel> Projects { get; }
    }
}