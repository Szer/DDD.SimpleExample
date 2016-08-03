using System;
using DDD.SimpleExample.Common.Events.Customer;

namespace DDD.SimpleExample.Domain.Customer.Events
{
    internal class CustomerMarkedAsInActive : ICustomerMarkedAsInActive
    {
        public CustomerMarkedAsInActive(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}