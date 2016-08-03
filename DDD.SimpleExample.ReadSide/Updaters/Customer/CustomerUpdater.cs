using System.Threading.Tasks;
using DDD.SimpleExample.Common.Enums;
using DDD.SimpleExample.Common.Events.Customer;
using DDD.SimpleExample.ReadSide.Helpers;
using DDD.SimpleExample.ReadSide.Interfaces;
using DDD.SimpleExample.ReadSide.Models;
using MassTransit;

namespace DDD.SimpleExample.ReadSide.Updaters.Customer
{
    internal class CustomerUpdater :
        IConsumer<ICustomerAdded>,
        IConsumer<ICustomerRenamed>,
        IConsumer<ICustomerMarkedAsInActive>

    {
        private readonly IModelUpdater _context;

        public CustomerUpdater(IModelUpdater context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ICustomerAdded> context)
        {
            var customer = new CustomerModel(context.Message.Id)
            {
                Name = context.Message.Name,
                Status = context.Message.Status
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            _context.Dispose();
        }

        public async Task Consume(ConsumeContext<ICustomerRenamed> context)
        {
            var customer = _context.Customers.FindAggregate(context.Message.Id);
            customer.Name = context.Message.NewName;
            await _context.SaveChangesAsync();
        }

        public async Task Consume(ConsumeContext<ICustomerMarkedAsInActive> context)
        {
            var customer = _context.Customers.FindAggregate(context.Message.Id);
            customer.Status = CustomerStatus.InActive;
            await _context.SaveChangesAsync();
        }
    }
}