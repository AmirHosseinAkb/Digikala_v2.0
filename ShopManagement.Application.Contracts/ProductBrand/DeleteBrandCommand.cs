using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductBrand
{
    public class DeleteBrandCommand
    {
        public long BrandId { get; set; }
        public string? BrandTitle { get; set; }
        public string? ImageName { get; set; }
    }
}
