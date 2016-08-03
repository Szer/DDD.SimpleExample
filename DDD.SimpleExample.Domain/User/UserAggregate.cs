using System;
using CommonDomain.Core;
using DDD.SimpleExample.Common;
using DDD.SimpleExample.Common.Enums;
using DDD.SimpleExample.Domain.User.Events;
using DDD.SimpleExample.Domain.User.ValueObjects;

namespace DDD.SimpleExample.Domain.User
{
    internal class UserAggregate : AggregateBase
    {
        private UserState _state;

        private UserAggregate(NonEmptyIdentity id)
        {
            Id = id;
        }

        internal UserAggregate(UserState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }
            _state = state;
        }

        public UserAggregate(NonEmptyIdentity id, UserName name) : this(id)
        {
            RaiseEvent(new UserAdded(id, name.FirstName, name.LastName, UserRole.Engineer));
        }

        public static UserAggregate Add(Guid id, string firstName, string lastName)
        {
            return new UserAggregate(new NonEmptyIdentity(id), new UserName(firstName, lastName));
        }

        public void AssignToProject(NonEmptyIdentity projectId)
        {
            if (_state.AssignedProjectIds.Contains(projectId))
            {
                throw new ArgumentException("Already assigned");
            }
            RaiseEvent(new UserAssignedToProject(Id, projectId));
        }

        public void Promote()
        {
            if (_state.Role == UserRole.Manager)
            {
                throw new ArgumentException("Cannot promote");
            }
            RaiseEvent(new UserPromoted(Id));
        }

        public void Demote()
        {
            if (_state.Role == UserRole.Engineer)
            {
                throw new ArgumentException("Cannot demote");
            }
            RaiseEvent(new UserDemoted(Id));
        }

        private void Apply(UserAdded @event)
        {
            _state = new UserState
            {
                Id = new NonEmptyIdentity(@event.Id),
                UserName = new UserName(@event.FirstName, @event.LastName),
                Role = @event.Role
            };
        }

        private void Apply(UserAssignedToProject @event)
        {
            _state.AssignedProjectIds.Add(new NonEmptyIdentity(@event.ProjectId));
        }

        private void Apply(UserPromoted @event)
        {
            _state.Role = UserRole.Manager;
        }

        private void Apply(UserDemoted @event)
        {
            _state.Role = UserRole.Engineer;
        }
    }
}