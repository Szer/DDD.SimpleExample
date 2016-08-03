using System;
using DDD.SimpleExample.Common.Events.Project;

namespace DDD.SimpleExample.Domain.Project.Events
{
    internal class ProjectRenamed : IProjectRenamed
    {
        public ProjectRenamed(Guid id, string newName, string oldName)
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