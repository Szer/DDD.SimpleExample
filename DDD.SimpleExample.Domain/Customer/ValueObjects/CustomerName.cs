using System;
using DDD.SimpleExample.Common;

namespace DDD.SimpleExample.Domain.Customer.ValueObjects
{
    internal class CustomerName : ValueObject<string>
    {
        public CustomerName(string name) : base(name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }
        }
    }
}
