using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DigikalaQuery.Contracts.ProductGroup
{
    public interface IProductGroupQuery
    {
        List<ProductGroupQueryModel> GetProductGroups();
    }
}
