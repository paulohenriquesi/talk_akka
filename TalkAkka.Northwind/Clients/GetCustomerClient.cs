using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TalkAkka.Northwind.Models;

namespace TalkAkka.Northwind
{
    public class GetCustomerClient : IGetCustomerClient
    {
        public HttpClient HttpClient { get; }

        public GetCustomerClient(HttpClient httpClient)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Customer> Get(string id)
        {
            var response = await HttpClient.GetAsync($"http://northwind.servicestack.net/customers/{id}.json");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetCustomerResponseModel>(content).Customer;
        }
    }
}
