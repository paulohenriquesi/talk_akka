using System;
using System.Collections.Generic;
using TalkAkka.Northwind.Models;

namespace TalkAkka.Northwind
{
    public class CustomerWithOrder
    {
        public Customer Customer { get; }
        public List<OrderResult> Orders { get; }

        public CustomerWithOrder(Customer customer, List<OrderResult> orders)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Orders = orders ?? throw new ArgumentNullException(nameof(orders));
        }
    }
}
