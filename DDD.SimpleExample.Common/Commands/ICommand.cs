using System;

namespace DDD.SimpleExample.Common.Commands
{
    public interface ICommand
    {
        DateTime Timestamp { get; }
        Guid CommandId { get; }
    }
}