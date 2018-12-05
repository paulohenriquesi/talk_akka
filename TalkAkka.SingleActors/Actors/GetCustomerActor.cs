using Akka.Actor;
using Akka.DI.Core;
using System;
using System.Threading.Tasks;
using TalkAkka.Northwind;
using TalkAkka.SingleActors.Messages;

namespace TalkAkka.SingleActors.Actors
{
    public class GetCustomerActor : ReceiveActor
    {
        private readonly IActorRef _getCustomerOrdersActor;
        public IGetCustomerClient GetCustomerClient { get; }

        public GetCustomerActor(IGetCustomerClient customerClient)
        {
            GetCustomerClient = customerClient ?? throw new ArgumentNullException(nameof(customerClient));
            _getCustomerOrdersActor = Context.ActorOf(Context.DI().Props<GetCustomersOrderActor>(), "GetCustomerOrders");

            ReceiveAsync<GetCustomer>(Get);
            Receive<Finished>(x => _getCustomerOrdersActor.Forward(x));
        }

        private async Task Get(GetCustomer msg)
        {
            var customer = await GetCustomerClient.Get(msg.CustomerSummary.Id);

            _getCustomerOrdersActor.Tell(new GetCustomerOrders(customer));
        }
    }
}
