using System;
using CommonDomain.Core;
using DDD.SimpleExample.Common;
using DDD.SimpleExample.Common.Enums;
using DDD.SimpleExample.Domain.Customer.Events;
using DDD.SimpleExample.Domain.Project.ValueObjects;
using ProjectAdded = DDD.SimpleExample.Domain.Project.Events.ProjectAdded;
using ProjectMarkedAsInActive = DDD.SimpleExample.Domain.Project.Events.ProjectMarkedAsInActive;
using ProjectRenamed = DDD.SimpleExample.Domain.Project.Events.ProjectRenamed;

namespace DDD.SimpleExample.Domain.Project
{
    internal class ProjectAggregate : AggregateBase
    {
        private ProjectState _state;

        private ProjectAggregate(NonEmptyIdentity id)
        {
            Id = id;
        }

        internal ProjectAggregate(ProjectState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }
            _state = state;
        }

        public ProjectAggregate(NonEmptyIdentity id, ProjectName name, NonEmptyIdentity customerId) : this(id)
        {
            RaiseEvent(new ProjectAdded(id, name, customerId, ProjectStatus.Active));
        }

        public static ProjectAggregate Add(Guid id, string name, Guid customerId)
        {
            return new ProjectAggregate(new NonEmptyIdentity(id), new ProjectName(name),
                new NonEmptyIdentity(customerId));
        }

        public void Rename(ProjectName newName)
        {
            if (_state.Status == ProjectStatus.InActive)
            {
                throw new InvalidOperationException("Project is inactive");
            }
            RaiseEvent(new ProjectRenamed(Id, newName, _state.Name));
        }

        public void MakeInActive()
        {
            if (_state.Status == ProjectStatus.InActive)
            {
                throw new InvalidOperationException("Project is inactive");
            }
            RaiseEvent(new ProjectMarkedAsInActive(Id));
        }

        private void Apply(ProjectAdded @event)
        {
            _state = new ProjectState
            {
                Id = new NonEmptyIdentity(Id),
                Name = new ProjectName(@event.Name),
                Status = @event.Status,
                CustomerId = new NonEmptyIdentity(@event.CustomerId)
            };
        }

        private void Apply(ProjectRenamed @event)
        {
            _state.Name = new ProjectName(@event.NewName);
        }

        private void Apply(ProjectMarkedAsInActive @event)
        {
            _state.Status = ProjectStatus.InActive;
        }
    }
}