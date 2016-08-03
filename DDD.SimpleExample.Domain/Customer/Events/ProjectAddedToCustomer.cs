using System;
using DDD.SimpleExample.Common.Events.Customer;

namespace DDD.SimpleExample.Domain.Customer.Events
{
    internal class ProjectAddedToCustomer : IProjectAddedToCustomer
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