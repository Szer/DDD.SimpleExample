using System;

namespace DDD.SimpleExample.Common.Commands.User
{
    public interface IAssignUserToProject : ICommand
    {
        Guid UserId { get; }
        Guid ProjectId { get; }
    }
}