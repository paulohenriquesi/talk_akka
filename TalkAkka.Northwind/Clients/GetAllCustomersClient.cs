using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TalkAkka.Northwind.Models;

namespace TalkAkka.Northwind
{
    public class GetAllCustomersClient : IGetAllCustomersClient
    {
        public HttpClient HttpClient { get; }

        public GetAllCustomersClient(HttpClient httpClient)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<CustomerSummary[]> GetAll()
        {
            var response = await HttpClient.GetAsync("http://northwind.servicestack.net/customers.json");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetCustomersResponseModel>(content).Customers;
        }
    }
}
