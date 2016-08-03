using System;
using DDD.SimpleExample.Common.Events.UserAdded;

namespace DDD.SimpleExample.Domain.User.Events
{
    internal class UserPromoted : IUserPromoted
    {
        public UserPromoted(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}