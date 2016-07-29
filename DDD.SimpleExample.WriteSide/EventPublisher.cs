using System;
using System.Configuration;
using System.Threading.Tasks;
using DDD.SimpleExample.Common.Events;
using MassTransit;
using SimpleInjector;

namespace DDD.SimpleExample.WriteSide
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IBusControl _bus;

        public EventPublisher(Container container)
        {
            _bus = container.GetInstance<IBusControl>();
        }

        public async Task PublishAsync(dynamic e)
        {
            var address = "rabbitmq://localhost/customer_events";
            var endpoint = await _bus.GetSendEndpoint(new Uri(address));
            await endpoint.Send(e);
        }
    }
}