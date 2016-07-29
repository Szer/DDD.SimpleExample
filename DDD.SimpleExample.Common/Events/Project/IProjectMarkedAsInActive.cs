using System;

namespace DDD.SimpleExample.Common.Events.Project
{
    public interface IProjectMarkedAsInActive : IEvent
    {
        Guid Id { get; }
    }
}