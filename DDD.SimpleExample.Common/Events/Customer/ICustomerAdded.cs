using System;
using DDD.SimpleExample.Common.Enums;

namespace DDD.SimpleExample.Common.Events.Customer
{
    public interface ICustomerAdded : IEvent
    {
        Guid Id { get; }
        string Name { get; }
        CustomerStatus Status { get; }
    }
}