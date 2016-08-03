using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DDD.SimpleExample.Application.API.Controllers
{
    public class OkActionResult<T> : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private readonly T _value;

        public OkActionResult(HttpRequestMessage request, T value)
        {
            _request = request;
            _value = value;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(HttpStatusCode.OK, _value);
            return Task.FromResult(response);
        }
    }
}