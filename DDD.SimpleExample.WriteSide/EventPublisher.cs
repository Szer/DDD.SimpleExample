using System;
using System.Threading.Tasks;
using DDD.SimpleExample.Common.Events;
using MassTransit;
using Ninject;

namespace DDD.SimpleExample.WriteSide
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IBusControl _bus;

        public EventPublisher(StandardKernel container)
        {
            _bus = container.Get<IBusControl>();
        }

        public async Task PublishAsync(dynamic e)
        {
            var address = "rabbitmq://localhost/events";
            var endpoint = await _bus.GetSendEndpoint(new Uri(address));
            await endpoint.Send(e);
        }
    }
}