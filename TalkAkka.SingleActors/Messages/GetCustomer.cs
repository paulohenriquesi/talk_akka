using System;
using TalkAkka.Northwind.Models;

namespace TalkAkka.SingleActors.Messages
{
    public class GetCustomer
    {
        public CustomerSummary CustomerSummary { get; }

        public GetCustomer(CustomerSummary customerSummary)
        {
            CustomerSummary = customerSummary ?? throw new ArgumentNullException(nameof(customerSummary));
        }
    }
}