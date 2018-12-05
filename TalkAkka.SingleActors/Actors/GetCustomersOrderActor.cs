using System;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using TalkAkka.Northwind;
using TalkAkka.Northwind.Clients;
using TalkAkka.SingleActors.Messages;

namespace TalkAkka.SingleActors.Actors
{
    class GetCustomersOrderActor : ReceiveActor
    {
        private readonly IActorRef _writeFileActor;
        public IGetCustomerOrdersClient GetCustomerOrdersClient { get; }

        public GetCustomersOrderActor(IGetCustomerOrdersClient getCustomerOrdersClient)
        {
            GetCustomerOrdersClient = getCustomerOrdersClient ?? throw new ArgumentNullException(nameof(getCustomerOrdersClient));
            _writeFileActor = Context.ActorOf(Context.DI().Props<WriteFileActor>(), "WriteFile");

            ReceiveAsync<GetCustomerOrders>(Get);
            Receive<Finished>(x => _writeFileActor.Forward(x));
        }

        private async Task Get(GetCustomerOrders msg)
        {
            var orderResults = await GetCustomerOrdersClient.Get(msg.Customer.Id);

            _writeFileActor.Tell(new AppendToFile(new CustomerWithOrder(msg.Customer, orderResults.ToList())));
        }
    }
}