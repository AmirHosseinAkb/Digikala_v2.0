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

    public OperationResult Create(CreateProductDiscountCommand command)
    {
        var result = new OperationResult();

        if (_productDiscountRepository.IsExistProductDiscount(command.ProductId, command.StartDate, command.EndDate))
            return result.Failed(ApplicationMessages.DuplicatedDiscount);

        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;
        try
        {
            startDate = command.StartDate.ShamsiToGerogorian();
        }
        catch
        {
            return result.Failed(ApplicationMessages.DateTimeFormatIsNotCorrect);
        }

        try
        {
            endDate=command.EndDate.ShamsiToGerogorian();
        }
        catch 
        {
            return result.Failed(ApplicationMessages.DateTimeFormatIsNotCorrect);
        }
        if(startDate>endDate)
            return result.Failed(ApplicationMessages.EndDateShouldBeGreaterThanStartDate);
        var discount = new ProductDiscount(command.ProductId, command.Rate, startDate, endDate);
        _productDiscountRepository.Add(discount);
        return result.Succeeded();
    }

    public EditProductDiscountCommand GetDiscountForEdit(long discountId)
    {
        var discount = _productDiscountRepository.GetDiscountById(discountId);
        return new EditProductDiscountCommand()
        {
            DiscountId = discount.ProductDiscountId,
            ProductId = discount.ProductId,
            Rate = discount.Rate,
            StartDate = discount.StartDate.ToShamsi(),
            EndDate = discount.EndDate.ToShamsi()
        };
    }

    public OperationResult Edit(EditProductDiscountCommand command)
    {
        var result = new OperationResult();
        var discount = _productDiscountRepository.GetDiscountById(command.DiscountId);
        if (command.ProductId != discount.ProductId)
        {
            if (_productDiscountRepository.IsExistProductDiscount(command.ProductId, command.StartDate, command.EndDate))
                return result.Failed(ApplicationMessages.DuplicatedDiscount);
        }

        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;

        try
        {
            startDate = command.StartDate.ShamsiToGerogorian();
        }
        catch
        {
            return result.Failed(ApplicationMessages.DateTimeFormatIsNotCorrect);
        }

        try
        {
            endDate=command.EndDate.ShamsiToGerogorian();
        }
        catch 
        {
            return result.Failed(ApplicationMessages.DateTimeFormatIsNotCorrect);
        }
        if(startDate>endDate)
            return result.Failed(ApplicationMessages.EndDateShouldBeGreaterThanStartDate);
        discount.Edit(command.ProductId,command.Rate,startDate,endDate);
        _productDiscountRepository.SaveChanges();
        return result.Succeeded();
    }

    public void Delete(long discountId)
    {
        var discount= _productDiscountRepository.GetDiscountById(discountId);
        if (discount != null)
        {
            discount.Delete();
            _productDiscountRepository.SaveChanges();
        }
    }
}