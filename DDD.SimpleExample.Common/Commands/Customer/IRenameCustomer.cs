using System;

namespace DDD.SimpleExample.Common.Commands.Customer
{
    public interface IRenameCustomer : ICommand
    {
        Guid Id { get; }

        string NewName { get; }
    }
}