using System;

namespace DDD.SimpleExample.Common.Commands.Project
{
    public interface IAddProjectToCustomer : ICommand
    {
        Guid Id { get; }

        string Name { get; }

        Guid CustomerGuid { get; }
    }
}