using System;
using DDD.SimpleExample.Common.Enums;
using DDD.SimpleExample.Common.Events.Customer;

namespace DDD.SimpleExample.Domain.Customer.Events
{
    internal class CustomerAdded : ICustomerAdded
    {
        public CustomerAdded(Guid id, string name, CustomerStatus status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

        public Guid Id { get; }
        public string Name { get; }
        public CustomerStatus Status { get; }
    }
}