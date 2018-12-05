using System;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using TalkAkka.GroupActors.Messages;
using TalkAkka.Northwind;
using TalkAkka.Northwind.Clients;

namespace TalkAkka.GroupActors.Actors
{
    class GetCustomersOrderActor : ReceiveActor
    {
        private IActorRef _writeFileActor;

        public IGetCustomerOrdersClient GetCustomerOrdersClient { get; }

        public GetCustomersOrderActor(IGetCustomerOrdersClient getCustomerOrdersClient)
        {
            GetCustomerOrdersClient = getCustomerOrdersClient ?? throw new ArgumentNullException(nameof(getCustomerOrdersClient));

            ReceiveAsync<GetCustomerOrders>(Get);
            Receive<Finished>(x => _writeFileActor.Forward(x));
        }

        private async Task Get(GetCustomerOrders msg)
        {
            _writeFileActor = msg.WriteFileActor;

            var orderResults = await GetCustomerOrdersClient.Get(msg.Customer.Id);

            _writeFileActor.Tell(new AppendToFile(new CustomerWithOrder(msg.Customer, orderResults.ToList())));
        }
    }
}