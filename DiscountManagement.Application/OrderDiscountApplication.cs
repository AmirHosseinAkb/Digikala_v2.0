using System.Text.RegularExpressions;
using _01_Framework.Application;
using DiscountManagement.Application.Contracts.OrderDiscount;
using DiscountManagement.Domain.OrderDiscountAgg;

namespace DiscountManagement.Application
{
    public class OrderDiscountApplication : IOrderDiscountApplication
    {
        private IOrderDiscountRepository _orderDiscountRepository;

        public OrderDiscountApplication(IOrderDiscountRepository orderDiscountRepository)
        {
            _orderDiscountRepository = orderDiscountRepository;
        }

        public OperationResult Create(CreateOrderDiscountCommand command)
        {
            var result = new OperationResult();
            DateTime? startDate = null;
            DateTime? endDate = null;

            if (_orderDiscountRepository.IsExistDiscount(command.DiscountCode))
                return result.Failed(ApplicationMessages.DuplicatedDiscount);

            if (!string.IsNullOrEmpty(command.StartDate))
            {
                try
                {
                    startDate = command.StartDate?.ShamsiToGerogorian();
                }
                catch
                {
                    return result.Failed(ApplicationMessages.DateTimeFormatIsNotCorrect);
                }

            }

            if (!string.IsNullOrEmpty(command.EndDate))
            {
                try
                {
                    endDate = command.EndDate?.ShamsiToGerogorian();
                }
                catch
                {
                    return result.Failed(ApplicationMessages.DateTimeFormatIsNotCorrect);
                }
            }

            if (!string.IsNullOrEmpty(command.StartDate) && !string.IsNullOrEmpty(command.EndDate))
            {
                if (DateTime.Parse(command.StartDate) > DateTime.Parse(command.EndDate))
                    return result.Failed(ApplicationMessages.EndDateShouldBeGreaterThanStartDate);
            }
                
            var discount = new OrderDiscount(command.DiscountCode.Replace(" ", ""), command.DiscountRate,
                command.Reason, command.UsableCount, startDate, endDate);
            _orderDiscountRepository.Add(discount);
            return result.Succeeded();
        }

        public Tuple<List<OrderDiscountViewModel>, int, int, int> GetOrderDiscounts(OrderDiscountSearchModel searchModel)
        {
            var discounts =
                _orderDiscountRepository.GetOrderDiscounts(searchModel.Code, searchModel.Reason, searchModel.IsActive);
            var take = searchModel.Take;
            int skip = (searchModel.PageId - 1) * take;
            int pageCount = discounts.Count() / take;
            if (discounts.Count() % take != 0)
                pageCount += 1;
            var query = discounts.Skip(skip).Take(take).Select(d => new OrderDiscountViewModel()
            {
                DiscountId = d.DiscountId,
                DiscountCode = d.DiscountCode,
                DiscountRate = d.DiscountRate.ToString(),
                StartDate = ((d.StartDate == null) ? null : d.StartDate.Value.ToShamsi()),
                EndDate = ((d.EndDate == null) ? null : d.EndDate.Value.ToShamsi()),
                UsableCount = d.UsableCount,
                Reason = d.Reason
            }).ToList();
            return Tuple.Create(query, searchModel.PageId, pageCount, searchModel.Take);

        }

        public EditOrderDiscountCommand GetDiscountForEdit(long discountId)
        {
            var discount = _orderDiscountRepository.GetOrderDiscount(discountId);
            return new EditOrderDiscountCommand()
            {
                DiscountId = discount.DiscountId,
                DiscountCode = discount.DiscountCode,
                DiscountRate = discount.DiscountRate,
                StartDate = discount.StartDate?.ToShamsi(),
                EndDate = discount.EndDate?.ToShamsi(),
                UsableCount = discount.UsableCount,
                Reason = discount.Reason
            };
        }

        public OperationResult Edit(EditOrderDiscountCommand command)
        {
            var result = new OperationResult();
            var discount = _orderDiscountRepository.GetOrderDiscount(command.DiscountId);
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (discount.DiscountCode != command.DiscountCode)
            {
                if (_orderDiscountRepository.IsExistDiscount(command.DiscountCode))
                    return result.Failed(ApplicationMessages.DuplicatedDiscount);
            }

            if (!string.IsNullOrEmpty(command.StartDate))
            {
                try
                {
                    startDate = command.StartDate?.ShamsiToGerogorian();
                }
                catch
                {
                    return result.Failed(ApplicationMessages.DateTimeFormatIsNotCorrect);
                }

            }

            if (!string.IsNullOrEmpty(command.EndDate))
            {
                try
                {
                    endDate = command.EndDate?.ShamsiToGerogorian();
                }
                catch
                {
                    return result.Failed(ApplicationMessages.DateTimeFormatIsNotCorrect);
                }
            }
            if (!string.IsNullOrEmpty(command.StartDate) && !string.IsNullOrEmpty(command.EndDate))
            {
                if (DateTime.Parse(command.StartDate) > DateTime.Parse(command.EndDate))
                    return result.Failed(ApplicationMessages.EndDateShouldBeGreaterThanStartDate);
            }
            discount.Edit(command.DiscountCode,command.DiscountRate,command.Reason,command.UsableCount,startDate,endDate);
            _orderDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public void Delete(long discountId)
        {
            var discount = _orderDiscountRepository.GetOrderDiscount(discountId);
            if (discount != null)
            {
                discount.Delete();
                _orderDiscountRepository.SaveChanges();
            }
        }
    }
}
