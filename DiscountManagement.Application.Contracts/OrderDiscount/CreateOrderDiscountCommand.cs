using _01_Framework.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Resources;
using Microsoft.VisualBasic.CompilerServices;

namespace DiscountManagement.Application.Contracts.OrderDiscount
{
    public class CreateOrderDiscountCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = ValidationMessages.MaxLength)]
        public string DiscountCode { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(0,100,ErrorMessage = ValidationMessages.IntegerRange)]
        public int DiscountRate { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = ValidationMessages.MaxLength)]
        public string Reason { get; set; }

        [Range(0,int.MaxValue,ErrorMessage = ValidationMessages.IntegerRange)]
        public int? UsableCount { get; set; }
        
        public string? StartDate { get; set; }

        public string? EndDate { get; set; }

    }
}
