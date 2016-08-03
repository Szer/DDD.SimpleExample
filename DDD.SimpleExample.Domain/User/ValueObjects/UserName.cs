using System;
using DDD.SimpleExample.Common;

namespace DDD.SimpleExample.Domain.User.ValueObjects
{
    public class UserName
    {
        public ValueObject<string> FirstName { get; }
        public ValueObject<string> LastName { get; }

        public UserName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException(firstName);
            }
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException(lastName);
            }
            FirstName = firstName;
            LastName = lastName;
        }
    }
}