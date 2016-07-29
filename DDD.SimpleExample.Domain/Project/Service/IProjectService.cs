using System;

namespace DDD.SimpleExample.Domain.Project.Service
{
    public interface IProjectService : IDomainService
    {
        void AddProjectToCustomer(Guid projectId, string name, Guid customerId);
        void MakeInActive(Guid projectId);
        void Rename(Guid projectId, string newName);
    }
}