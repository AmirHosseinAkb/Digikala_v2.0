using _01_Framework.Application;
using DiscountManagement.Application.Contracts.ProductDiscount;
using DiscountManagement.Domain.ProductDiscountAgg;
using ShopManagement.Domain.ProductAgg;

namespace DiscountManagement.Application;

public class ProductDiscountApplication:IProductDiscountApplication
{
    private readonly IProductDiscountRepository _productDiscountRepository;
    private readonly IProductRepository _productRepository;

    public ProductDiscountApplication(IProductDiscountRepository productDiscountRepository, IProductRepository productRepository)
    {
        _productDiscountRepository = productDiscountRepository;
        _productRepository = productRepository;
    }
    public Tuple<List<ProductDiscountViewModel>, int, int, int> GetProductDiscounts(ProductDiscountSearchModel searchModel)
    {
        var discounts =
            _productDiscountRepository.GetProductDiscounts(searchModel.ProductId, searchModel.StartDate,
                searchModel.EndDate);
        var take = searchModel.Take;
        var skip = (searchModel.PageId - 1) * take;
        var pageCount = discounts.Count() / take;
        if (discounts.Count() % take != 0)
            pageCount += 1;
        var query = discounts.Skip(skip).Take(take).Select(d => new ProductDiscountViewModel()
        {
            DiscountId = d.ProductDiscountId,
            Rate = d.Rate,
            StartDate = d.StartDate.ToShamsi(),
            EndDate = d.EndDate.ToShamsi(),
            ProductTitle = _productRepository.GetProductById(d.ProductId).Title
        }).ToList();
        return Tuple.Create(query, searchModel.PageId, pageCount, searchModel.Take);
    }
}