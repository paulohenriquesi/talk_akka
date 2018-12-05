using System;
using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.SimpleInjector;
using SimpleInjector;
using TalkAkka.GroupActors.Actors;
using TalkAkka.GroupActors.Messages;
using TalkAkka.Northwind;

namespace TalkAkka.GroupActors
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.AddNorthwindClients();
            container.AddActors();

            var system = ActorSystem.Create("SingleActors");

            new SimpleInjectorDependencyResolver(container, system);

            var getAllCustomersActor = system.ActorOf(system.DI().Props<GetAllCustomersActor>(), "GetAllCustomers");

            getAllCustomersActor.Tell(new GetAllCustomers());

            Console.Read();
        }
    }
}
