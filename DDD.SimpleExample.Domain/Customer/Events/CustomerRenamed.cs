using System;
using DDD.SimpleExample.Common.Events.Customer;

namespace DDD.SimpleExample.Domain.Customer.Events
{
    public class CustomerRenamed : ICustomerRenamed
    {
        public CustomerRenamed(Guid id, string newName, string oldName)
        {
            Id = id;
            NewName = newName;
            OldName = oldName;
        }

        public Guid Id { get; }
        public string NewName { get; }
        public string OldName { get; }
    }
}