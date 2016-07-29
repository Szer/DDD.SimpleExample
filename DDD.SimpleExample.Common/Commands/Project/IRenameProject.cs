using System;

namespace DDD.SimpleExample.Common.Commands.Project
{
    public interface IRenameProject : ICommand
    {
        Guid Id { get; }

        string NewName { get; }
    }
}