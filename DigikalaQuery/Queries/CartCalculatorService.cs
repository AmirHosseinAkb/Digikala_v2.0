using DigikalaQuery.Contracts.Services;
using DiscountManagement.Infrastructure.EfCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Infrastructure.EfCore;

namespace DigikalaQuery.Queries;

public class CartCalculatorService:ICartCalculatorService
{
    private readonly ShopContext _shopContext;
    private readonly DiscountContext _discountContext;
    public CartCalculatorService(ShopContext shopContext, DiscountContext discountContext)
    {
        _shopContext = shopContext;
        _discountContext = discountContext;
    }
    public Cart ComputeCart(List<CartItem> cartItems)
    {
        var inventories = _shopContext.Inventories.Select(i => new {i.ProductId, i.ProductCount}).ToList();
        var cart = new Cart();
        foreach (var cartItem in cartItems)
        {
            if (inventories.Any(i => i.ProductId == cartItem.Id && i.ProductCount >= cartItem.Count))//This Section Changed
            {
                cartItem.IsInStock = true;
            }

            var product = _shopContext.Products.Find(cartItem.Id);
            if (product != null)
            {
                cartItem.Title = product!.Title;
                cartItem.UnitPrice = product!.Price;
                cartItem.TotalItemPrice = cartItem.UnitPrice * cartItem.Count;
                cartItem.DiscountRate = _discountContext.ProductDiscounts
                    .SingleOrDefault(d => d.ProductId == cartItem.Id)?.Rate;
                if (cartItem.DiscountRate != null)
                {
                    cartItem.DiscountPrice = (cartItem.DiscountRate.Value * cartItem.TotalItemPrice)/100;
                }
                cartItem.PayingPrice = cartItem.TotalItemPrice - cartItem.DiscountPrice;
            }
            cart.Add(cartItem);
        }
        return cart;
    }
}