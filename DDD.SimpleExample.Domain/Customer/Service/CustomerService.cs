using System;
using CommonDomain.Persistence;
using DDD.SimpleExample.Domain.Customer.ValueObjects;

namespace DDD.SimpleExample.Domain.Customer.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository _repository;

        public CustomerService(IRepository repository)
        {
            _repository = repository;
        }
        public void Add(Guid id, string name)
        {
            var customer = CustomerAggregate.Add(id, name);
            _repository.Save(customer, Guid.NewGuid(), null);
        }

        public void Rename(Guid id, string newName)
        {
            var customer = _repository.GetById<CustomerAggregate>(id);
            customer.Rename(new CustomerName(newName));
            _repository.Save(customer, Guid.NewGuid(), null);
        }

        public void MakeInActive(Guid id)
        {
            var customer = _repository.GetById<CustomerAggregate>(id);
            customer.MakeInActive();
            _repository.Save(customer, Guid.NewGuid(), null);
        }
    }
}