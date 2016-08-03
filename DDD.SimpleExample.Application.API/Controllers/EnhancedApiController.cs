using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using MassTransit;

namespace DDD.SimpleExample.Application.API.Controllers
{
    public abstract class EnhancedApiController : ApiController
    {
        protected IHttpActionResult Accepted<T>(T value)
        {
            return new AcceptedActionResult<T>(Request, value);
        }

        protected async Task Send<TMessage>(TMessage message,
            CancellationToken cancellationToken = default(CancellationToken))
            where TMessage : class
        {
            var address = "rabbitmq://localhost/commands";
            var endpoint = await WebApiApplication.Bus.GetSendEndpoint(new Uri(address));
            await endpoint.Send<TMessage>(message, cancellationToken);
        }

        protected async Task<TResponse> SendRequest<TRequest, TResponse>(TRequest request,
            CancellationToken cancellationToken = default(CancellationToken))
            where TRequest : class
            where TResponse : class
        {
            var address = new Uri("rabbitmq://localhost/requests");
            var requestTimeout = TimeSpan.FromSeconds(30);

            IRequestClient<TRequest, TResponse> client =
                new MessageRequestClient<TRequest, TResponse>(WebApiApplication.Bus, address, requestTimeout);
            return await client.Request(request, cancellationToken);
        }
    }
}