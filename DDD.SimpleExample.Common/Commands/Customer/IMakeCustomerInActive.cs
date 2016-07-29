using System;

namespace DDD.SimpleExample.Common.Commands.Customer
{
    public interface IMakeCustomerInActive : ICommand
    {
        Guid Id { get; }
    }
}