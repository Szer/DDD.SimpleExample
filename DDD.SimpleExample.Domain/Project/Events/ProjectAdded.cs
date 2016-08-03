using System;
using DDD.SimpleExample.Common.Enums;
using DDD.SimpleExample.Common.Events.Project;

namespace DDD.SimpleExample.Domain.Project.Events
{
    internal class ProjectAdded : IProjectAdded
    {
        public ProjectAdded(Guid id, string name, Guid customerId, ProjectStatus status)
        {
            Id = id;
            Name = name;
            CustomerId = customerId;
            Status = status;
        }

        public Guid Id { get; }
        public string Name { get; }
        public Guid CustomerId { get; }
        public ProjectStatus Status { get; }
    }
}