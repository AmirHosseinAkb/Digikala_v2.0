using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contracts.ProductBrand;
using ShopManagement.Domain.ProductBrandAgg;

namespace ShopManagement.Application
{
    public class ProductBrandApplication:IProductBrandApplication
    {
        private readonly IProductBrandRepository _productBrandRepository;

        public ProductBrandApplication(IProductBrandRepository productBrandRepository)
        {
            _productBrandRepository = productBrandRepository;
        }
        public Tuple<List<ProductBrandViewModel>, int, int, int> GetProductBrands(int pageId = 1, string title = "", int take = 20)
        {
            var brands = _productBrandRepository.GetProductBrands(title);
            int skip = (pageId - 1) * take;
            int pageCount = brands.Count() / take;

            if (brands.Count() % take != 0)
                pageCount+=1;

            var query = brands.Skip(skip).Take(take).Select(b => new ProductBrandViewModel()
            {
                BrandId = b.BrandId,
                ImageName = b.ImageName,
                BrandTitle = b.BrandTitle,
                OtherLangTitle = b.OtherLangTitle
            }).ToList();
            return Tuple.Create(query, pageId, pageCount, take);
        }

        public OperationResult Create(CreateBrandCommand command)
        {
            var result = new OperationResult();
            if (_productBrandRepository.IsExistBrand(command.Title, command.OtherLangTitle))
                return result.Failed(ApplicationMessages.DuplicatedBrand);
            string imageName = "";
            if (command.BrandImage != null)
            {
                imageName = CodeGenerator.GenerateUniqName() + Path.GetExtension(command.BrandImage.FileName);
                string imagePath = Directories.ProductBrandDirectory(imageName);
                using(var stream=new FileStream(imagePath,FileMode.Create))
                    command.BrandImage.CopyTo(stream);
            }
            var brand = new ProductBrand(command.Title, command.OtherLangTitle,imageName);
            _productBrandRepository.Add(brand);
            return result.Succeeded();
        }

        public EditBrandCommand GetBrandForEdit(long brandId)
        {
            var brand = _productBrandRepository.GetBrand(brandId);
            return new EditBrandCommand()
            {
                BrandId = brand.BrandId,
                OtherLangTitle = brand.OtherLangTitle,
                Title = brand.BrandTitle,
                ImageName = brand.ImageName
            };
        }

        public OperationResult Edit(EditBrandCommand command)
        {
            var result= new OperationResult();
            var brand = _productBrandRepository.GetBrand(command.BrandId);
            if (command.Title != brand.BrandTitle || command.OtherLangTitle != brand.OtherLangTitle)
            {
                if (_productBrandRepository.IsExistBrand(command.Title, command.OtherLangTitle))
                    return result.Failed(ApplicationMessages.DuplicatedBrand);
            }

            string imageName = brand.ImageName;
            if (command.BrandImage != null)
            {
                var currentImage = Directories.ProductBrandDirectory(brand.ImageName);
                if (File.Exists(currentImage))
                    File.Delete(currentImage);

                imageName = CodeGenerator.GenerateUniqName() + Path.GetExtension(command.BrandImage.FileName);
                var imagePath = Directories.ProductBrandDirectory(imageName);
                using(var stream=new FileStream(imagePath,FileMode.Create))
                    command.BrandImage.CopyTo(stream);
            }
            brand.Edit(command.Title,command.OtherLangTitle,imageName);
            _productBrandRepository.SaveChanges();
            return result.Succeeded();
        }
    }
}
