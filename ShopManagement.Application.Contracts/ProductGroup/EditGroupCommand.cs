using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductGroup
{
    public class EditGroupCommand:CreateGroupCommand
    {
        public long GroupId { get; set; }
        public string? ImageName { get; set; }
    }
}
