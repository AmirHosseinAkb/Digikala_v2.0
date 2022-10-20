using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigikalaQuery.Contracts.Order
{
    public interface IOrderQuery
    {
        Tuple<List<OrderQueryModel>,List<OrderQueryModel>,List<OrderQueryModel>> GetUserOrders();
    }
}
