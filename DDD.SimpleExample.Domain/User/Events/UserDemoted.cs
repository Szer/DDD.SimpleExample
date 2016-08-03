using System;
using DDD.SimpleExample.Common.Events.UserAdded;

namespace DDD.SimpleExample.Domain.User.Events
{
    internal class UserDemoted : IUserDemoted
    {
        public UserDemoted(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}