using System;
using TalkAkka.Northwind;

namespace TalkAkka.GroupActors.Messages
{
    public class AppendToFile
    {
        public CustomerWithOrder CustomerWithOrder { get; }

        public AppendToFile(CustomerWithOrder customerWithOrder)
        {
            CustomerWithOrder = customerWithOrder ?? throw new ArgumentNullException(nameof(customerWithOrder));
        }
    }
}