using System;
using System.Threading.Tasks;
using System.Web.Http;
using DDD.SimpleExample.Application.API.Models;
using DDD.SimpleExample.Common.Commands.Customer;
using DDD.SimpleExample.Common.DTOs;

namespace DDD.SimpleExample.Application.API.Controllers
{
    public class CustomerController : EnhancedApiController
    {
        // POST api/customer
        public async Task<IHttpActionResult> Post(AddCustomerRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AddCustomer(model);
            await Send(command);

            return Accepted(new AddCustomerResult
            {
                AddCustomerRequestId = model.AddCustomerRequestId,
                Timestamp = command.Timestamp
            });
        }

        public IHttpActionResult Get(int id)
        {
            return Accepted(id);
        }

        public IHttpActionResult Get()
        {
            return Accepted("Fine");
        }
    }

    class AddCustomer : IAddCustomer
    {
        private readonly AddCustomerRequest _customer;

        public AddCustomer(AddCustomerRequest customer)
        {
            _customer = customer;
            Timestamp = DateTime.UtcNow;
        }

        public Guid Id => _customer.Id;
        public string Name => _customer.Name;
        public DateTime Timestamp { get; }
        public Guid CommandId => _customer.AddCustomerRequestId;
    }
}