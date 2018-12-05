using Akka.Actor;
using System;
using TalkAkka.Northwind.Models;

namespace TalkAkka.GroupActors.Messages
{
    public class GetCustomer
    {
        public IActorRef WriteFileActor { get; }
        public CustomerSummary CustomerSummary { get; }

        public GetCustomer(IActorRef writeFileActor, CustomerSummary customerSummary)
        {
            WriteFileActor = writeFileActor ?? throw new ArgumentNullException(nameof(writeFileActor));
            CustomerSummary = customerSummary ?? throw new ArgumentNullException(nameof(customerSummary));
        }
    }
}