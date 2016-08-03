using System;

namespace DDD.SimpleExample.Common.Events.UserAdded
{
    public interface IUserPromoted
    {
        Guid UserId { get; }
    }
}