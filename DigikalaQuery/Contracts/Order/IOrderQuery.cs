using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigikalaQuery.Contracts.Order
{
    public interface IOrderQuery
    {
        List<OrderQueryModel> GetUserOrders();
    }
}
