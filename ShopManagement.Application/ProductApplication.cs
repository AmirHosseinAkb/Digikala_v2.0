using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using Org.BouncyCastle.Asn1.Misc;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductGroup;
using ShopManagement.Domain.InventoryAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductGroupAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public ProductApplication(IProductRepository productRepository, IProductGroupRepository productGroupRepository, IInventoryRepository inventoryRepository)
        {
            _productRepository = productRepository;
            _productGroupRepository = productGroupRepository;
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateProductCommand command)
        {
            var result = new OperationResult();
            if (_productRepository.IsExistProduct(command.Title))
                return result.Failed(ApplicationMessages.DuplicatedProduct);

            bool isCorrect = CheckInputGroups(command.GroupId, command.PrimaryGroupId, command.SecondaryGroupId);
            if (!isCorrect)
                return result.Failed(ApplicationMessages.ProcessFailed);

            string produtImage = DefaultImages.DefaultProductImage;

            if (command.ProductImage != null)
            {
                produtImage = CodeGenerator.GenerateUniqName() + Path.GetExtension(command.ProductImage.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Products",
                    "Images",
                    produtImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    command.ProductImage.CopyTo(stream);
                }
            }

            var product = new Product(command.GroupId,command.BrandId, command.PrimaryGroupId, command.SecondaryGroupId, command.Title,
                command.Description, command.Price, command.OtherLangTitle, command.Tags, produtImage);
            
            long productId=_productRepository.AddProduct(product);
            var inventory = new Inventory(productId, 0);
            _inventoryRepository.AddInventory(inventory);
            _productRepository.AddProductDetails(product);
            return result.Succeeded();
        }

        public EditProductCommand GetProductForEdit(long productId)
        {
            var product = _productRepository.GetProductById(productId);
            return new EditProductCommand()
            {
                Title = product.Title,
                Description = product.Description,
                GroupId = product.GroupId,
                BrandId = product.BrandId,
                ImageName = product.ImageName,
                OtherLangTitle = product.OtherLangTitle,
                Price = product.Price,
                Tags = product.Tags,
                PrimaryGroupId = product.PrimaryGroupId,
                SecondaryGroupId = product.SecondaryGroupId
            };
        }

        public OperationResult Edit(EditProductCommand command)
        {
            var result = new OperationResult();
            var product = _productRepository.GetProductById(command.ProductId);
            if (product.Title != command.Title)
            {
                if (_productRepository.IsExistProduct(command.Title))
                    return result.Failed(ApplicationMessages.DuplicatedProduct);
            }

            bool isCorrect = CheckInputGroups(command.GroupId, command.PrimaryGroupId, command.SecondaryGroupId);
            if (!isCorrect)
                return result.Failed(ApplicationMessages.ProcessFailed);

            var productImage = product.ImageName;
            if (command.ProductImage != null)
            {
                string imagePath = "";
                if (product.ImageName != DefaultImages.DefaultProductImage)
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Products",
                        "Images",
                        product.ImageName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                productImage = CodeGenerator.GenerateUniqName() + Path.GetExtension(command.ProductImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Products",
                    "Images",
                    productImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    command.ProductImage.CopyTo(stream);
                }
            }

            bool isGroupChanged = command.GroupId != product.GroupId || command.PrimaryGroupId != product.PrimaryGroupId || command
                .SecondaryGroupId != product.SecondaryGroupId;

            product.Edit(command.GroupId,command.BrandId, command.PrimaryGroupId, command.SecondaryGroupId, command.Title
            , command.Description, command.Price, command.OtherLangTitle, command.Tags, productImage);


            if (isGroupChanged)
                _productRepository.EditProductDetails(product);

            _productRepository.SaveChanges();
            return result.Succeeded();
        }

        public DeleteProductCommand GetProductForDelete(long productId)
        {
            var product = _productRepository.GetProductWithGroups(productId);
            return new DeleteProductCommand()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                GroupName = product.ProductGroup.GroupTitle,
                PrimaryGroupName = product.PrimaryProductGroup?.GroupTitle,
                SecondaryGroupName = product.SecondaryProductGroup?.GroupTitle,
                ImageName = product.ImageName
            };
        }

        public OperationResult Delete(long productId)
        {
            var result = new OperationResult();
            if (productId == 0)
                return result.Failed(ApplicationMessages.RecordNotFound);
            var product = _productRepository.GetProductById(productId);
            if (product == null)
                return result.NullResult();
            product.Delete();

            var imagePath = Directories.ProductDirectory(product.ImageName);
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            _productRepository.SaveChanges();
            return result.Succeeded();
        }

        public bool IsExistProductByGroup(long groupId)
        {
            var product = _productRepository.GetProductByGroupId(groupId);
            if (product != null)
                return true;
            return false;
        }

        public bool CheckInputGroups(long groupId, long? primaryGroupId, long? secondaryGroupId)
        {
            var group = _productGroupRepository.GetGroupById(groupId);

            ProductGroup? primaryGroup = null;
            if (primaryGroupId != null)
            {
                primaryGroup = _productGroupRepository.GetGroupById(primaryGroupId.Value);
                if (primaryGroup == null)
                    return false;
            }

            ProductGroup? secondaryGroup = null;
            if (secondaryGroupId != null)
            {
                secondaryGroup = _productGroupRepository.GetGroupById(secondaryGroupId.Value);
                if (secondaryGroup == null)
                    return false;
            }

            if (_productGroupRepository.GetSubGroups(groupId).Any() && primaryGroup == null)
                return false;

            if (primaryGroup != null)
            {
                if (primaryGroup.ParentId != group.GroupId)
                    return false;

                if (_productGroupRepository.GetSubGroups(primaryGroupId.Value).Any() && secondaryGroup == null)
                    return false;
            }

            if (secondaryGroup != null)
            {
                if (secondaryGroup.ParentId != primaryGroup.GroupId)
                    return false;
            }

            if (primaryGroupId == null && secondaryGroupId != null)
                return false;
            return true;
        }

        public List<ProductDetailViewModel> GetProductDetails(long productId)
        {
            return _productRepository.GetProductDetails(productId).Select(d => new ProductDetailViewModel()
            {
                DetailId = d.DetailId,
                DetailTitle = d.GroupDetail.DetailTitle,
                DetailValue = d.DetailValue
            }).ToList();
        }

        public OperationResult ConfirmProductDetails(long productId, Dictionary<int, string> details)
        {
            var result = new OperationResult();
            var detailsList=_productRepository.GetProductDetails(productId).Select(d => d.DetailId).ToList();

            if (details.Count != detailsList.Count)
                return result.Failed(ApplicationMessages.ProcessFailed);
            foreach (var key in details.Keys)
            {
                if (!detailsList.Contains(key))
                    return result.Failed(ApplicationMessages.ProcessFailed);
            }

            _productRepository.ConfirmProductDetails(productId, details);
            return result.Succeeded();
        }


        public Tuple<List<ProductViewModel>, int, int, int> GetProducts(int pageId = 1, string title = "", long groupId = 0, long primaryGroupId = 0, long secondaryGroupId = 0, int take = 0)
        {
            var products = _productRepository.GetAll(title, groupId, primaryGroupId, secondaryGroupId).Select(p => new ProductViewModel()
            {
                ProductId = p.ProductId,
                Title = p.Title,
                ImageName = p.ImageName,
                CreationDate = p.CreationDate.ToShamsi(),
                Price = p.Price
            }).ToList();

            var pageCount = products.Count() / take;

            if (products.Count() % take != 0)
                pageCount++;

            var skip = (pageId - 1) * take;

            return Tuple.Create(products, pageId, pageCount, take);
        }


    }
}
