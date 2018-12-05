using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TalkAkka.Northwind;
using TalkAkka.Northwind.Clients;

namespace TalkAkka.OldJob
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var container = new Container();
            container.AddNorthwindClients();

            var getCustomersClient = container.GetInstance<IGetAllCustomersClient>();
            var getCustomerClient = container.GetInstance<IGetCustomerClient>();
            var getCustomerOrdersClient = container.GetInstance<IGetCustomerOrdersClient>();

            var file = new List<CustomerWithOrder>();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            
            var customerSummaries = await getCustomersClient.GetAll();

            foreach (var c in customerSummaries)
            {
                var customer = await getCustomerClient.Get(c.Id);
                var orders = await getCustomerOrdersClient.Get(c.Id);

                file.Add(new CustomerWithOrder(customer, orders.ToList()));
            }

            var fileContent = JsonConvert.SerializeObject(file);

            File.WriteAllText($@"C:\github\paulohenriquesi\talk_akka\files\{DateTime.Now:yyyy-MM-ddTHH-mm-ss}customers.json", fileContent);

            stopWatch.Stop();

            Console.WriteLine($"Escrevi geral em {stopWatch.ElapsedMilliseconds}ms");
            Console.Read();
        }
    }
}
