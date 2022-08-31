using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductBrandAgg;

namespace ShopManagement.Domain.ProductGroupAgg
{
    public class ProductGroup
    {
        public long GroupId { get;private set; }
        public string GroupTitle { get; private set; }
        public long? ParentId { get; private set; }
        public List<ProductGroup> ProductGroups { get;private set; }
        public string? ImageName { get;private set; }

        public List<Product> Products { get;private set; }
        public List<Product> PrimaryProducts { get;private set; }
        public List<Product> SecondaryProducts { get;private set; }
        public List<GroupDetail> GroupDetails { get;private set; }

        protected ProductGroup()
        {
            
        }
        public ProductGroup(string groupTitle,long? parentId,string? imageName)
        {
            GroupTitle=groupTitle;
            ParentId=parentId;
            ImageName = imageName;
        }

        public void Edit(string groupTitle, long? parentId, string? imageName)
        {
            GroupTitle = groupTitle;
            ParentId = parentId;
            ImageName = imageName;
        }
    }
}
