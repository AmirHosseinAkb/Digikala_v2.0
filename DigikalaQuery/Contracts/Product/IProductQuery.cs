﻿using _01_Framework.Application;
using DigikalaQuery.Contracts.ProductBrand;
using DigikalaQuery.Contracts.ProductColors;
using DiscountManagement.Domain.ProductDiscountAgg;
using ShopManagement.Application.Contracts.Order;

namespace DigikalaQuery.Contracts.Product
{
    public interface IProductQuery
    {
        Tuple<List<ProductBoxQueryModel>,List<ProductColorQueryModel>,List<ProductBrandQueryModel>, int, int,int> GetProductsForShow(SearchProductQueryModel searchModel);

        Tuple<List<ProductBoxQueryModel>, int, int> GetProductsList(SearchProductQueryModel searchModel);
        ProductQueryModel? GetProduct(long productId);
        ProductDiscount? GetProductCurrentDiscount(long productId);
        List<CartItemViewModel> CheckInventoryStatus(List<CartItemViewModel> cartItems);
        OperationResult ChangeItemCount(List<CartItemViewModel> cartItems,Guid guid, int count);

    }
}
