using System;

namespace DDD.SimpleExample.Common.Events.UserAdded
{
    public interface IUserAssignedToProject
    {
        Guid UserId { get; }
        Guid ProjectId { get; }
    }
}