using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;
using TalkAkka.GroupActors.Messages;
using TalkAkka.Northwind;

namespace TalkAkka.GroupActors.Actors
{
    public class GetAllCustomersActor : ReceiveActor
    {
        private readonly IActorRef _getCustomerActor;
        private readonly IActorRef _writeFileActor;
        private readonly Stopwatch _stopwatch;

        public IGetAllCustomersClient GetAllCustomersClient { get; }

        public GetAllCustomersActor(IGetAllCustomersClient allCustomersClient)
        {
            GetAllCustomersClient = allCustomersClient ?? throw new ArgumentNullException(nameof(allCustomersClient));
            
            _stopwatch = new Stopwatch();
            _getCustomerActor =
                Context.ActorOf(Context.DI().Props<GetCustomerActor>().WithRouter(new RoundRobinPool(10)),
                    "GetCustomersPool");

            _writeFileActor = Context.ActorOf(Context.DI().Props<WriteFileActor>(), "WriteFileActor");

            Become(Ready);
        }

        private void Ready()
        {
            _stopwatch.Reset();
            ReceiveAsync<GetAllCustomers>(x => GetAllCustomers());
        }

        private void Waiting()
        {
            Receive<Finished>(x => Finished());
        }

        private async Task GetAllCustomers()
        {
            _stopwatch.Start();

            var customers = await GetAllCustomersClient.GetAll();

            foreach (var c in customers)
            {
                _getCustomerActor.Tell(new GetCustomer(_writeFileActor, c));
            }

            _getCustomerActor.Tell(new Broadcast(new Finished()));

            Become(Waiting);
        }

        private void Finished()
        {
            _stopwatch.Stop();
            Console.WriteLine($"Escrevi geral em {_stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
