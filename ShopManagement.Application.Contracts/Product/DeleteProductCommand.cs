using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Product
{
    public class DeleteProductCommand
    {
        public DeleteProductCommand()
        {
            
        }
        public long ProductId { get; set; }
        public string Title { get; set; }
        public string GroupName { get; set; }
        public string? PrimaryGroupName { get; set; }
        public string? SecondaryGroupName { get; set; }
        public string? ImageName { get; set; }
    }
    
}
