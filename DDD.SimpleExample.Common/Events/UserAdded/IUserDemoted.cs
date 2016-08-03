using System;

namespace DDD.SimpleExample.Common.Events.UserAdded
{
    public interface IUserDemoted
    {
        Guid UserId { get; }
    }
}