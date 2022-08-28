using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contracts.ProductColor;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductColorAgg;

namespace ShopManagement.Application
{
    public class ProductColorApplication:IProductColorApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductColorRepository _productColorRepository;

        public ProductColorApplication(IProductRepository productRepository, IProductColorRepository productColorRepository)
        {
            _productRepository = productRepository;
            _productColorRepository = productColorRepository;
        }
        public OperationResult Create(CreateColorCommand command)
        {
            var result = new OperationResult();
            var product = _productRepository.GetProductById(command.ProductId);

            if (product == null)
                return result.NullResult();

            if (_productColorRepository.IsExistColor(command.ProductId, command.ColorName, command.ColorCode))
                return result.Failed(ApplicationMessages.DuplicatedColor);
            var color = new ProductColor(command.ProductId, command.ColorName, command.ColorCode);
            _productColorRepository.Add(color);
            return result.Succeeded();
        }

        public List<ProductColorViewModel> GetAll()
        {
            return _productColorRepository.GetAll().Select(c => new ProductColorViewModel()
            {
                ColorId = c.ColorId,
                ProductId = c.ProductId,
                ColorName = c.ColorName,
                ColorCode = c.ColorCode
            }).ToList();
        }

        public void Delete(long colorId)
        {
            var color = _productColorRepository.GetById(colorId);
            if(color!=null)
                _productColorRepository.Delete(color);
        }
    }
}
