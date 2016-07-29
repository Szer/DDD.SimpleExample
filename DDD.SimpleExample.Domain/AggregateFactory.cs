using System;
using System.Reflection;
using CommonDomain;
using CommonDomain.Persistence;
using DDD.SimpleExample.Common;

namespace DDD.SimpleExample.Domain
{
    public class AggregateFactory : IConstructAggregates
    {
        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            ConstructorInfo constructor = type.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null, new[] {typeof(NonEmptyIdentity)}, null);

            return constructor.Invoke(new object[] {new NonEmptyIdentity(id)}) as IAggregate;
        }
    }
}