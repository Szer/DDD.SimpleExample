using System;

namespace DDD.SimpleExample.Common.Events.Project
{
    public interface IProjectRenamed : IEvent
    {
        Guid Id { get; }
        string NewName { get; }
        string OldName { get; }
    }
}