using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;

namespace DiscountManagement.Application.Contracts.ProductDiscount
{
    public class CreateProductDiscountCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(0,100,ErrorMessage = ValidationMessages.IntegerRange)]
        public int Rate { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartDate { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndDate { get; set; }
    }
}
