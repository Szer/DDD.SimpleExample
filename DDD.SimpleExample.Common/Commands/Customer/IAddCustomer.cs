using System;

namespace DDD.SimpleExample.Common.Commands.Customer
{
    public interface IAddCustomer : ICommand
    {
        Guid Id { get; }

        string Name { get; }
    }
}