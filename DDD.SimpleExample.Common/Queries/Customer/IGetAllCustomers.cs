using System.Collections.Generic;

namespace DDD.SimpleExample.Common.Queries.Customer
{
    public interface IGetAllCustomers
    {
    }

    public interface IGetAllCustomersResult
    {
        List<DTOs.Customer> Customers { get; }
    }
}