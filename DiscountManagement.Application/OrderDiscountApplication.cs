using System.Text.RegularExpressions;
using _01_Framework.Application;
using DiscountManagement.Application.Contracts.OrderDiscount;
using DiscountManagement.Domain.OrderDiscountAgg;

namespace DiscountManagement.Application
{
    public class OrderDiscountApplication:IOrderDiscountApplication
    {
        private IOrderDiscountRepository _orderDiscountRepository;

        public OrderDiscountApplication(IOrderDiscountRepository orderDiscountRepository)
        {
            _orderDiscountRepository = orderDiscountRepository;
        }
        public OperationResult Create(CreateOrderDiscountCommand command)
        {
            var result=new OperationResult();
            DateTime? startDate=null;
            DateTime? endDate=null;

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
                
            var discount = new OrderDiscount(command.DiscountCode.Replace(" ", ""), command.DiscountRate,
                command.Reason, command.UsableCount, startDate, endDate);
            _orderDiscountRepository.Add(discount);
            return result.Succeeded();
        }
    }
}
