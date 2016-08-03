using System;

namespace DDD.SimpleExample.Common.Commands.User
{
    public interface IPromoteUser : ICommand
    {
        Guid UserId { get; }
    }
}