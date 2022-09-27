using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;

namespace DiscountManagement.Application.Contracts.OrderDiscount
{
    public class OrderDiscountSearchModel:SearchModelsMain
    {
        public string? Code { get; set; } = "";
        public string? Reason { get; set; } = "";
        public bool IsActive { get; set; } = false;
    }
}
