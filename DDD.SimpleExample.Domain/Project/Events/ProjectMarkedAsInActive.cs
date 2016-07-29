using System;
using DDD.SimpleExample.Common.Events;
using DDD.SimpleExample.Common.Events.Project;

namespace DDD.SimpleExample.Domain.Project.Events
{
    public class ProjectMarkedAsInActive : IProjectMarkedAsInActive
    {
        public ProjectMarkedAsInActive(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}