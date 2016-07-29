using System;

namespace DDD.SimpleExample.Common.Events.Customer
{
    public interface ICustomerMarkedAsInActive : IEvent
    {
        Guid Id { get; }
    }
}