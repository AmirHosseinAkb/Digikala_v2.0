﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductBrandAgg
{
    public interface IProductBrandRepository
    {
        IQueryable<ProductBrand> GetProductBrands(string title="");
        bool IsExistBrand(string title,string otherlangTitle);
        void Add(ProductBrand productBrand);
    }
}
