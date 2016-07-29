using System;
using DDD.SimpleExample.Common;

namespace DDD.SimpleExample.Domain.Project.ValueObjects
{
    public class ProjectName : ValueObject<string>
    {
        public ProjectName(string name) : base(name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }
        }
    }
}