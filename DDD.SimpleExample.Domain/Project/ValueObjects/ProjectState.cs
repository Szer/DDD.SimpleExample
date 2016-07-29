using DDD.SimpleExample.Common;
using DDD.SimpleExample.Common.Enums;
using DDD.SimpleExample.Domain.Customer.ValueObjects;

namespace DDD.SimpleExample.Domain.Project.ValueObjects
{
    public class ProjectState
    {
        public NonEmptyIdentity Id { get; set; }
        public NonEmptyIdentity CustomerId { get; set; }
        public ProjectStatus Status { get; set; }
        public ProjectName Name { get; set; }
        public CustomerState CustomerState { get; set; }
    }
}