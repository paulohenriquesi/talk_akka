using System;
using Akka.Actor;
using TalkAkka.Northwind.Models;

namespace TalkAkka.GroupActors.Messages
{
    public class GetCustomerOrders
    {
        public IActorRef WriteFileActor { get; }
        public Customer Customer { get; }

        public GetCustomerOrders(IActorRef writeFileActor, Customer customer)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            WriteFileActor = writeFileActor ?? throw new ArgumentNullException(nameof(writeFileActor));
        }
    }
}