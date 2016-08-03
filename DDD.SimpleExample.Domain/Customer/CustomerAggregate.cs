using System;
using CommonDomain.Core;
using DDD.SimpleExample.Common;
using DDD.SimpleExample.Common.Enums;
using DDD.SimpleExample.Domain.Customer.Events;
using DDD.SimpleExample.Domain.Customer.ValueObjects;
using DDD.SimpleExample.Domain.Project.Events;

namespace DDD.SimpleExample.Domain.Customer
{
    internal class CustomerAggregate : AggregateBase
    {
        private CustomerState _state;

        private CustomerAggregate(NonEmptyIdentity id)
        {
            Id = id;
        }

        internal CustomerAggregate(CustomerState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }
            _state = state;
        }

        public CustomerAggregate(NonEmptyIdentity id, CustomerName name) : this(id)
        {
            RaiseEvent(new CustomerAdded(id, name, CustomerStatus.Active));
        }

        public static CustomerAggregate Add(Guid id, string name)
        {
            return new CustomerAggregate(new NonEmptyIdentity(id), new CustomerName(name));
        }

        public void Rename(CustomerName newName)
        {
            if (_state.Status == CustomerStatus.InActive)
            {
                throw new InvalidOperationException("CustomerAggregate is inactive");
            }
            RaiseEvent(new CustomerRenamed(Id, newName, _state.Name));
        }

        public void MakeInActive()
        {
            if (_state.Status == CustomerStatus.InActive)
            {
                throw new InvalidOperationException("CustomerAggregate is inactive");
            }

            RaiseEvent(new CustomerMarkedAsInActive(Id));
            foreach (var identity in _state.ProjectIds)
            {
                RaiseEvent(new ProjectMarkedAsInActive(identity));
            }
        }

        //public void AddProjectAssociation(NonEmptyIdentity projectId)
        //{
        //    if (_state.Status == CustomerStatus.InActive)
        //    {
        //        throw new InvalidOperationException("CustomerAggregate is inactive");
        //    }
        //    RaiseEvent(new ProjectAddedToCustomer(projectId, Id));
        //}

        private void Apply(CustomerAdded @event)
        {
            _state = new CustomerState
            {
                Id = new NonEmptyIdentity(Id),
                Name = new CustomerName(@event.Name),
                Status = @event.Status
            };
        }

        private void Apply(CustomerRenamed @event)
        {
            _state.Name = new CustomerName(@event.NewName);
        }

        private void Apply(CustomerMarkedAsInActive @event)
        {
            _state.Status = CustomerStatus.InActive;
        }

        private void Apply(ProjectAddedToCustomer @event)
        {
            _state.ProjectIds.Add(new NonEmptyIdentity(@event.ProjectId));
        }
    }
}