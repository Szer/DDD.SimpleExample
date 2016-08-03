using System;
using System.Threading.Tasks;
using MassTransit;
using Ninject;

namespace DDD.SimpleExample.ReadSide
{
    internal class ScopeObserver : IReceiveObserver
    {
        public class ScopeObject { }

        private readonly StandardKernel _kernel;

        public ScopeObject Current { get; private set; }

        public ScopeObserver(StandardKernel kernel)
        {
            _kernel = kernel;
        }

        public Task PreReceive(ReceiveContext context)
        {
            Current = new ScopeObject();
            return Task.FromResult(0);
        }

        public Task PostReceive(ReceiveContext context)
        {
            Current = null;
            return Task.FromResult(0);
        }

        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            Current = null;
            return Task.FromResult(0);
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType,
            Exception exception) where T : class
        {
            Current = null;
            return Task.FromResult(0);
        }

        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            Current = null;
            return Task.FromResult(0);
        }
    }
}