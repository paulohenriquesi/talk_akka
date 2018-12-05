using System.Threading.Tasks;
using TalkAkka.Northwind.Models;

namespace TalkAkka.Northwind
{
    public interface IGetCustomerClient
    {
        Task<Customer> Get(string id);
    }
}