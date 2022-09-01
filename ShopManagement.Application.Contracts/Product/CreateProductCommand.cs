using _01_Framework.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProductCommand
    {
        public CreateProductCommand()
        {
            
        }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public long GroupId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public long BrandId { get; set; }

        public long? PrimaryGroupId { get; set; }

        public long? SecondaryGroupId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = ValidationMessages.MaxLength)]

        public string Title { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200,ErrorMessage = ValidationMessages.MaxLength)]
        public string OtherLangTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(100,int.MaxValue,ErrorMessage = ValidationMessages.IntegerRange)]
        public int Price { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(300,ErrorMessage = ValidationMessages.MaxLength)]
        public string Tags { get; set; }

        public IFormFile? ProductImage { get; set; }

        
    }
}
