using System;
using DDD.SimpleExample.Common.Enums;

namespace DDD.SimpleExample.Common.Events.Project
{
    public interface IProjectAdded : IEvent
    {
        Guid Id { get; }
        string Name { get; }
        Guid CustomerId { get; }
        ProjectStatus Status { get; }
    }
}