﻿using System.Reflection.Metadata;
using DigikalaQuery.Contracts.ProductBrand;
using DigikalaQuery.Contracts.ProductColors;

namespace DigikalaQuery.Contracts.Product
{
    public interface IProductQuery
    {
        Tuple<List<ProductBoxQueryModel>,List<ProductColorQueryModel>,List<ProductBrandQueryModel>, int, int,int> GetProductsForShow(SearchProductQueryModel searchModel);

        Tuple<List<ProductBoxQueryModel>, int, int> GetProductsList(SearchProductQueryModel searchModel);

        
    }
}
