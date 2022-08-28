using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductColor
{
    public class CreateColorCommand
    {
        public long ProductId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200,ErrorMessage = ValidationMessages.MaxLength)]
        public string ColorName { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200,ErrorMessage = ValidationMessages.MaxLength)]
        public string ColorCode { get; set; }
    }
}
