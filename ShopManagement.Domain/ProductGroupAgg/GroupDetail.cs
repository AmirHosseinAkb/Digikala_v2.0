using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductGroupAgg
{
    public class GroupDetail
    {
        public long DetailId { get;private set; }
        public long GroupId { get;private set; }
        public string DetailTitle { get;private set; }

        public ProductGroup ProductGroup { get;private set; }
        public List<ProductDetail> ProductDetails { get;private set; }

        protected GroupDetail()
        {
            
        }

        public GroupDetail(long groupId, string detailTitle)
        {
            GroupId = groupId;
            DetailTitle = detailTitle;
        }

        public void Edit(string detailTitle)
        {
            DetailTitle=detailTitle;
        }
    }
}
