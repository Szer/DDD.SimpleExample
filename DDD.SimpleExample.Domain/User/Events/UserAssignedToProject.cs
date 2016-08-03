using System;
using DDD.SimpleExample.Common.Events.UserAdded;

namespace DDD.SimpleExample.Domain.User.Events
{
    internal class UserAssignedToProject : IUserAssignedToProject
    {
        public UserAssignedToProject(Guid userId, Guid projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }

        public Guid UserId { get; }
        public Guid ProjectId { get; }
    }
}