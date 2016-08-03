using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DDD.SimpleExample.Common.DTOs;
using DDD.SimpleExample.Common.Queries.Customer;
using DDD.SimpleExample.ReadSide.Interfaces;
using MassTransit;

namespace DDD.SimpleExample.ReadSide.Readers
{
    internal class CustomerReader :
        IConsumer<IGetAllCustomers>,
        IConsumer<IGetCustomer>
    {
        private readonly IModelReader _reader;
        private readonly IMapper _mapper;

        public CustomerReader(IModelReader reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IGetAllCustomers> context)
        {
            var customers = _reader.Customers
                .Include(x=>x.Projects)
                .Select(_mapper.Map<Customer>)
                .ToList();
            var respond = new GetAllCustomerResult(customers);
            await context.RespondAsync(respond);
        }

        public async Task Consume(ConsumeContext<IGetCustomer> context)
        {
            var customer = _mapper.Map<Customer>(_reader.Customers
                .Include(x => x.Projects)
                .FirstOrDefault(x => x.AggregateId == context.Message.Id));
            var respond = new GetCustomerResult(customer);
            await context.RespondAsync(respond);
        }

        class GetAllCustomerResult : IGetAllCustomersResult
        {
            public GetAllCustomerResult(List<Customer> customers)
            {
                Customers = customers;
            }

            public List<Customer> Customers { get; }
        }

        class GetCustomerResult : IGetCustomerResult
        {
            public GetCustomerResult(Customer customer)
            {
                Customer = customer;
            }

            public Customer Customer { get; }
        }
    }
}