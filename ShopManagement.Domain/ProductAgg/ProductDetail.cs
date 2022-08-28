using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductGroupAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class ProductDetail
    {
        public long PD_Id { get;private set; }
        public long ProductId { get;private set; }
        public long DetailId { get;private set; }
        public string? DetailValue { get;private set; }
        public Product Product { get;private set; }
        public GroupDetail GroupDetail { get;private set; }

        public ProductDetail(long productId, long detailId, string? detailValue)
        {
            ProductId = productId;
            DetailId = detailId;
            DetailValue = detailValue;
        }

        public void Edit(string detailValue)
        {
            DetailValue = detailValue;
        }
    }
}
