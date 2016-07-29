using System;

namespace DDD.SimpleExample.Domain.Customer.Service
{
    public interface ICustomerService : IDomainService
    {
        void Add(Guid id, string name);
        void Rename(Guid id, string newName);
        void MakeInActive(Guid id);
    }
}