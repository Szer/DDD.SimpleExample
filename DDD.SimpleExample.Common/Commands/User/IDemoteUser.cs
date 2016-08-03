using System;

namespace DDD.SimpleExample.Common.Commands.User
{
    public interface IDemoteUser : ICommand
    {
        Guid UserId { get; }
    }
}