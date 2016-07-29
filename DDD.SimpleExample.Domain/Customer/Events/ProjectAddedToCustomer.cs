using System;
using DDD.SimpleExample.Common.Events;
using DDD.SimpleExample.Common.Events.Customer;

namespace DDD.SimpleExample.Domain.Customer.Events
{
    public class ProjectAddedToCustomer : IProjectAddedToCustomer
    {
        public ProjectAddedToCustomer(Guid projectId, Guid customerId)
        {
            ProjectId = projectId;
            CustomerId = customerId;
        }

        public Guid ProjectId { get; }
        public Guid CustomerId { get; }
    }
}