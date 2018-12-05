using System.Threading.Tasks;
using TalkAkka.Northwind.Models;

namespace TalkAkka.Northwind
{
    public interface IGetAllCustomersClient
    {
        Task<CustomerSummary[]> GetAll();
    }
}