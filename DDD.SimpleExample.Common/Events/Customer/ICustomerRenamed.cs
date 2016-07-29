using System;

namespace DDD.SimpleExample.Common.Events.Customer
{
    public interface ICustomerRenamed : IEvent
    {
        Guid Id { get; }
        string NewName { get; }
        string OldName { get; }
    }
}