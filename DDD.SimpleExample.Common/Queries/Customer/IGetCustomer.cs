using System;

namespace DDD.SimpleExample.Common.Queries.Customer
{
    public interface IGetCustomer
    {
        Guid Id { get; }
    }

    public interface IGetCustomerResult
    {
        DTOs.Customer Customer { get; }
    }
}