using System;

namespace DDD.SimpleExample.Common
{
    public class ValueObject<T> : IEquatable<T>
    {
        public T Value { get; }

        public ValueObject(T value = default(T))
        {
            Value = value;
        }
        public bool HasValue => Value != null && !Value.Equals(default(T));

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            T other = (T) obj;

            return Equals(other);
        }

        public bool Equals(T other)
        {
            return Equals(new ValueObject<T>(other));
        }

        public virtual bool Equals(ValueObject<T> other)
        {
            return HasValue && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return !HasValue ? "" : Value.ToString();
        }

        public static implicit operator T(ValueObject<T> x)
        {
            return x.Value;
        }

        public static implicit operator ValueObject<T>(T x)
        {
            return new ValueObject<T>(x);
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return x.Equals(y);
        }

        public static bool operator ==(ValueObject<T> x, T y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return x.Equals(new ValueObject<T>(y));
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }

        public static bool operator !=(ValueObject<T> x, T y)
        {
            return !(x == y);
        }
    }
}