﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;

namespace DiscountManagement.Application.Contracts.ProductDiscount
{
    public class ProductDiscountSearchModel:SearchModelsMain
    {
        public long ProductId { get; set; } = 0;
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
    }
}
