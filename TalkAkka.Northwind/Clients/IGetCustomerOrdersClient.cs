using System.Threading.Tasks;
using TalkAkka.Northwind.Models;

namespace TalkAkka.Northwind.Clients
{
    public interface IGetCustomerOrdersClient
    {
        Task<OrderResult[]> Get(string customerId);
    }
}