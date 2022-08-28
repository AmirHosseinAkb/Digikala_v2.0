using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Product
{
    public class EditProductCommand:CreateProductCommand
    {
        public long ProductId { get; set; }
        public string? ImageName { get; set; }
    }
}
