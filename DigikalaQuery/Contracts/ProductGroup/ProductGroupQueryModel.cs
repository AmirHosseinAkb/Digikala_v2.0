using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigikalaQuery.Contracts.ProductGroup
{
    public class ProductGroupQueryModel
    {
        public long GroupId { get; set; }
        public string GroupTitle { get; set; }
        public long? ParentId { get; set; }
        public string? ImageName { get; set; }
    }
}
