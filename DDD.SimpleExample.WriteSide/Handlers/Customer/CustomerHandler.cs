using System.Threading.Tasks;
using DDD.SimpleExample.Common.Commands.Customer;
using DDD.SimpleExample.Domain.Customer.Service;
using MassTransit;

namespace DDD.SimpleExample.WriteSide.Handlers.Customer
{
    internal class CustomerHandler :
        IConsumer<IAddCustomer>,
        IConsumer<IMakeCustomerInActive>,
        IConsumer<IRenameCustomer>
    {
        private readonly ICustomerService _service;

        public CustomerHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<IAddCustomer> context)
        {
            await Task.Run(() => _service.Add(context.Message.Id, context.Message.Name));
        }

        public async Task Consume(ConsumeContext<IMakeCustomerInActive> context)
        {
            await Task.Run(() => _service.MakeInActive(context.Message.Id));
        }

        public async Task Consume(ConsumeContext<IRenameCustomer> context)
        {
            await Task.Run(() => _service.Rename(context.Message.Id, context.Message.NewName));
        }
    }
}