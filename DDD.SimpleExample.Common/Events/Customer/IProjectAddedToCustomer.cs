using System;

namespace DDD.SimpleExample.Common.Events.Customer
{
    public interface IProjectAddedToCustomer : IEvent
    {
        Guid ProjectId { get; }
        Guid CustomerId { get; }
    }
}