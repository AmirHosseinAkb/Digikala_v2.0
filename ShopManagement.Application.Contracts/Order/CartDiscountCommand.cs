using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.Order
{
    public class CartDiscountCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = ValidationMessages.MaxLength)]
        public string DiscountCode { get; set; }
    }
}
