using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TalkAkka.Northwind.Clients;
using TalkAkka.Northwind.Models;

namespace TalkAkka.Northwind
{
    public class GetCustomerOrdersClient : IGetCustomerOrdersClient
    {
        public HttpClient HttpClient { get; }

        public GetCustomerOrdersClient(HttpClient httpClient)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<OrderResult[]> Get(string customerId)
        {
            var response = await HttpClient.GetAsync($"http://northwind.servicestack.net/customers/{customerId}/orders.json");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetOrdersResponseModel>(content).Results;
        }
    }
}
