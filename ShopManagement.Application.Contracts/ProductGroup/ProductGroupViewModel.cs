using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductGroup
{
    public class ProductGroupViewModel
    {
        public long GroupId { get; set; }
        public string? ImageName { get; set; }
        public string GroupTitle { get; set; }
        public long? ParentId { get; set; }

    }
}
