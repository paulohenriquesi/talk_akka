using System;
using TalkAkka.Northwind.Models;

namespace TalkAkka.SingleActors.Messages
{
    public class GetCustomerOrders
    {
        public Customer Customer { get; }

        public GetCustomerOrders(Customer customer)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }
    }
}