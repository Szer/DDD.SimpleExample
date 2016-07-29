using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DDD.SimpleExample.Application.API.Controllers
{
    public abstract class EnhancedApiController : ApiController
    {
        protected IHttpActionResult Accepted<T>(T value)
        {
            return new AcceptedActionResult<T>(Request, value);
        }

        protected async Task Send<T>(T message, CancellationToken cancellationToken = default(CancellationToken))
            where T : class
        {
            var address = "rabbitmq://localhost/customer";
            var endpoint = await WebApiApplication.Bus.GetSendEndpoint(new Uri(address));
            await endpoint.Send<T>(message, cancellationToken);
        }
    }
}