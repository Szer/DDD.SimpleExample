using System.Collections.Generic;
using DDD.SimpleExample.Common;
using DDD.SimpleExample.Common.Enums;

namespace DDD.SimpleExample.Domain.Customer.ValueObjects
{
    internal class CustomerState
    {
        public NonEmptyIdentity Id { get; set; }
        public CustomerStatus Status { get; set; }
        public CustomerName Name { get; set; }
        public List<NonEmptyIdentity> ProjectIds { get; set; } = new List<NonEmptyIdentity>();
    }
}