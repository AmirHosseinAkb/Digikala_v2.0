using DigikalaQuery.Contracts.Product;
using DigikalaQuery.Contracts.ProductBrand;
using DigikalaQuery.Contracts.ProductColors;
using DigikalaQuery.Contracts.ProductDetail;
using DigikalaQuery.Contracts.ProductImage;
using DiscountManagement.Domain.ProductDiscountAgg;
using DiscountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace DigikalaQuery.Queries
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext shopContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _discountContext = discountContext;
        }
        public Tuple<List<ProductBoxQueryModel>, List<ProductColorQueryModel>, List<ProductBrandQueryModel>, int, int, int> GetProductsForShow
        (SearchProductQueryModel searchModel)
        {
            var products = _shopContext.Products.OrderByDescending(p => p.CreationDate)
                .Include(p => p.Inventory)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductBrand)
                .AsQueryable()
                .AsNoTracking();

            if (!string.IsNullOrEmpty(searchModel.Title))
            {
                products = products.Where(p => p.Title.Contains(searchModel.Title)
                                               || p.OtherLangTitle.Contains(searchModel.Title)
                                               || p.Tags.Contains(searchModel.Title));
            }
            if (searchModel.GroupId != 0)
            {

                products = products.Where(p =>
                    p.GroupId == searchModel.GroupId || p.PrimaryGroupId == searchModel.GroupId || p.SecondaryGroupId == searchModel.GroupId);
            }

            int take = 12;

            int skip = (searchModel.PageId - 1) * take;

            var pageCount = products.Count() / take;

            if (products.Count() % take != 0)
                pageCount += 1;

            var productColors = products.SelectMany(p => p.ProductColors)
                .GroupBy(c => c.ColorName).Select(c => c.First()).ToList();

            var productBrands = products.Select(p => p.ProductBrand)
                .GroupBy(b => b.BrandTitle).Select(b => b.First()).ToList();

            var maxPrice = 0;
            if (products.Any())
                maxPrice = products.Max(p => p.Price);

            var query = products.Skip(skip).Take(take).Select(p => new ProductBoxQueryModel()
            {
                ProductId = p.ProductId,
                Title = p.Title,
                ImageName = p.ImageName,
                Price = p.Price,
                InventoryCount = p.Inventory.ProductCount,
                ProductColors = p.ProductColors,

            }).ToList();
            foreach (var product in query)
            {
                product.DiscountRate = GetProductCurrentDiscount(product.ProductId)?.Rate;
            }
            return Tuple.Create(
            query,
            productColors.Select(c => new ProductColorQueryModel()
            {
                ColorId = c.ColorId,
                ColorName = c.ColorName,
                ColorCode = c.ColorCode
            }).ToList(),
            productBrands.Distinct().Select(b => new ProductBrandQueryModel()
            {
                BrandId = b.BrandId,
                BrandTitle = b.BrandTitle,
                OtherLangTitle = b.OtherLangTitle
            }).ToList(),
            searchModel.PageId, pageCount, maxPrice);
        }

        public Tuple<List<ProductBoxQueryModel>, int, int> GetProductsList(SearchProductQueryModel searchModel)
        {
            var products = _shopContext.Products.OrderByDescending(p => p.CreationDate)
                .Include(p => p.Inventory)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductBrand)
                .AsQueryable().
                AsNoTracking();

            switch (searchModel.OrderBy)
            {
                case "newest":
                    {
                        products = products.OrderByDescending(p => p.CreationDate);
                        break;
                    }
                case "bestSelling":
                    {
                        //Do SomeThing
                        break;
                    }
                case "popular":
                    {
                        //Do SomeThing
                        break;
                    }
                case "mostExpensive":
                    {
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    }
                case "cheapest":
                    {
                        products = products.OrderBy(p => p.Price);
                        break;
                    }
                default:
                    {
                        products = products.OrderByDescending(p => p.CreationDate);
                        break;
                    }
            }
            if (searchModel.GroupId != 0)
            {

                products = products.Where(p =>
                    p.GroupId == searchModel.GroupId || p.PrimaryGroupId == searchModel.GroupId || p.SecondaryGroupId == searchModel.GroupId);
            }

            if (searchModel.IsInStock)
                products = products.Where(p => p.Inventory.ProductCount > 0);

            if (searchModel.Brands.Any())
            {
                foreach (var brandId in searchModel.Brands)
                {
                    products = products.Where(p => p.BrandId == brandId);
                }
            }

            if (searchModel.Colors.Any())
            {
                foreach (var colorName in searchModel.Colors)
                {
                    products = products.Where(p => p.ProductColors.Any(c => c.ColorName == colorName));
                }
            }

            if (searchModel.StartPrice != 0)
                products = products.Where(p => p.Price >= searchModel.StartPrice);

            if (products.Any())
            {
                if (searchModel.EndPrice != products.Max(p => p.Price))
                    products = products.Where(p => p.Price <= searchModel.EndPrice);
            }
            int take = 12;

            int skip = (searchModel.PageId - 1) * take;

            var pageCount = products.Count() / take;

            if (products.Count() % take != 0)
                pageCount += 1;
            var query = products
                .Skip(skip)
                .Take(take)
                .Select(p => new ProductBoxQueryModel()
                {
                    ProductId = p.ProductId,
                    Title = p.Title,
                    ImageName = p.ImageName,
                    Price = p.Price,
                    InventoryCount = p.Inventory.ProductCount,
                    ProductColors = p.ProductColors
                }).ToList();
            foreach (var product in query)
            {
                product.DiscountRate = GetProductCurrentDiscount(product.ProductId)?.Rate;
            }
            return Tuple.Create(
                query, searchModel.PageId, pageCount);
        }

        public ProductQueryModel? GetProduct(long productId)
        {
            var product = _shopContext.Products
                .Include(p => p.ProductGroup)
                .Include(p => p.PrimaryProductGroup)
                .Include(p => p.SecondaryProductGroup)
                .Include(p => p.ProductBrand)
                .Include(p => p.Inventory)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductDetails)
                .ThenInclude(d => d.GroupDetail)
                .SingleOrDefault(p => p.ProductId == productId);
            if (product == null)
                return null;
            var productColors = product.ProductColors.Select(c => new ProductColorQueryModel()
            {
                ColorId = c.ColorId,
                ColorName = c.ColorName,
                ColorCode = c.ColorCode
            });
            var productDetails = product.ProductDetails.Select(d => new ProductDetailQueryModel()
            {
                DetailTitle = d.GroupDetail.DetailTitle,
                DetailValue = d.DetailValue
            });
            var productImages = product.ProductImages.Select(i => new ProductImageQueryModel()
            {
                ImageName = i.ImageName
            });
            return new ProductQueryModel()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                OtherLangTitle = product.OtherLangTitle,
                GroupTitle = product.ProductGroup.GroupTitle,
                PrimaryGroupTitle = product.PrimaryProductGroup?.GroupTitle,
                SecondaryGroupTitle = product.SecondaryProductGroup?.GroupTitle,
                Description = product.Description,
                ImageName = product.ImageName,
                BrandName = product.ProductBrand.BrandTitle,
                InventoryCount = product.Inventory.ProductCount,
                Price = product.Price,
                DiscountRate = GetProductCurrentDiscount(product.ProductId)?.Rate,
                ProductColors = productColors.ToList(),
                ProductImages = productImages.ToList(),
                ProductDetails = productDetails.ToList()
            };
        }

        public ProductDiscount? GetProductCurrentDiscount(long productId)
        {
            return _discountContext.ProductDiscounts.SingleOrDefault(d =>
                d.ProductId == productId && d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now);
        }
    }
}
