using SimpleInjector;
using System.Net.Http;
using TalkAkka.Northwind.Clients;

namespace TalkAkka.Northwind
{
    public static class Module
    {
        public static void AddNorthwindClients(this Container container)
        {
            container.RegisterInstance(new HttpClient());

            container.Register<IGetAllCustomersClient, GetAllCustomersClient>();
            container.Register<IGetCustomerClient, GetCustomerClient>();
            container.Register<IGetCustomerOrdersClient, GetCustomerOrdersClient>();
        }
    }
}
