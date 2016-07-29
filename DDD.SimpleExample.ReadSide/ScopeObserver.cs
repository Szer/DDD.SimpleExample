using System;
using System.Threading.Tasks;
using MassTransit;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace DDD.SimpleExample.ReadSide
{
    internal class ScopeObserver : IReceiveObserver
    {
        private readonly Container _container;
        private Scope _scope;

        public ScopeObserver(Container container)
        {
            _container = container;
        }

        public Task PreReceive(ReceiveContext context)
        {
            _scope = _container.BeginExecutionContextScope();
            return Task.FromResult(0);
        }

        public Task PostReceive(ReceiveContext context)
        {
            _scope?.Dispose();
            _scope = null;
            return Task.FromResult(0);
        }

        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            _scope?.Dispose();
            _scope = null;
            return Task.FromResult(0);
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType,
            Exception exception) where T : class
        {
            _scope?.Dispose();
            _scope = null;
            return Task.FromResult(0);
        }

        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            _scope?.Dispose();
            _scope = null;
            return Task.FromResult(0);
        }
    }
}