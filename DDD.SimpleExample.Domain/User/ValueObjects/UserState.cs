using System.Collections.Generic;
using DDD.SimpleExample.Common;
using DDD.SimpleExample.Common.Enums;

namespace DDD.SimpleExample.Domain.User.ValueObjects
{
    public class UserState
    {
        public NonEmptyIdentity Id { get; set; }
        public UserName UserName { get; set; }
        public UserRole Role { get; set; }
        public List<NonEmptyIdentity> AssignedProjectIds { get; set; } = new List<NonEmptyIdentity>();
    }
}