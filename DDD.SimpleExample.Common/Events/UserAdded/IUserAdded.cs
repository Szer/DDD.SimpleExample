using System;
using DDD.SimpleExample.Common.Enums;

namespace DDD.SimpleExample.Common.Events.UserAdded
{
    public interface IUserAdded
    {
        Guid Id { get; }
        string FirstName { get; }
        string LastName { get; }
        UserRole Role { get; }
    }
}