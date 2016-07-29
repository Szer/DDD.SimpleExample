using System;

namespace DDD.SimpleExample.Common.Commands.Project
{
    public interface IMakeProjectInActive : ICommand
    {
        Guid Id { get; }
    }
}