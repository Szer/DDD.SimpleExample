using System;

namespace DDD.SimpleExample.Common
{
    public class NonEmptyIdentity : ValueObject<Guid>
    {
        public NonEmptyIdentity(Guid id) : base(id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
        }
    }
}
