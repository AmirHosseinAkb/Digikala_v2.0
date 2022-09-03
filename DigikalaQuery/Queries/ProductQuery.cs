using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigikalaQuery.Contracts.Product;
using DigikalaQuery.Contracts.ProductBrand;
using DigikalaQuery.Contracts.ProductColors;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace DigikalaQuery.Queries
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;

        public ProductQuery(ShopContext context)
        {
            _context = context;
        }
        public Tuple<List<ProductBoxQueryModel>, List<ProductColorQueryModel>, List<ProductBrandQueryModel>, int, int> GetProductsForShow
        (int pageId = 1, string title = "", string orderBy = "", bool isInStock = false,
            int startPrice = 0, int endPrice = 0, List<int> brands = null, List<int> colors = null)
        {
            var products = _context.Products
                .Include(p => p.Inventory)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductBrand)
                .AsQueryable().
                AsNoTracking();

            if (!string.IsNullOrEmpty(title))
            {
                products = products.Where(p => p.Title.Contains(title));
            }

            switch (orderBy)
            {
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
                case "newest":
                    {
                        products = products.OrderByDescending(p => p.CreationDate);
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
            }
            if (isInStock)
                products = products.Where(p => p.Inventory.ProductCount > 0);


            if (brands.Any())
            {
                foreach (var brandId in brands)
                {
                    products = products.Where(p => p.BrandId == brandId);
                }
            }

            if (colors.Any())
            {
                foreach (var colorId in colors)
                {
                    products = products.Where(p => p.ProductColors.Any(c => c.ColorId == colorId));
                }
            }

            int take = 12;

            int skip = (pageId - 1) * take;

            var pageCount = products.Count() / take;

            if (products.Count() % take != 0)
                pageCount += 1;

            var query = products.Skip(skip).Take(take);

            if (startPrice != 0)
                products = products.Where(p => p.Price > startPrice);
            if (query.Any())
            {
                if (endPrice != query.Max(p => p.Price))
                    products = products.Where(p => p.Price < endPrice);
            }


            var productColors = _context.ProductColors.AsQueryable();
            foreach (var product in query)
            {
                productColors = productColors.Where(c => c.ProductId == product.ProductId);
            }

            var productBrands = query.Select(p => p.ProductBrand);

            return Tuple.Create(
                query.Select(p => new ProductBoxQueryModel()
                {
                    ProductId = p.ProductId,
                    Title = p.Title,
                    ImageName = p.ImageName,
                    Price = p.Price,
                    ProductColors = p.ProductColors
                }).ToList(),
                productColors.Select(c => new ProductColorQueryModel()
                {
                    ColorId = c.ColorId,
                    ColorName = c.ColorName,
                    ColorCode = c.ColorCode
                }).ToList(),
                productBrands.Select(b => new ProductBrandQueryModel()
                {
                    BrandId = b.BrandId,
                    BrandTitle = b.BrandTitle,
                    OtherLangTitle = b.OtherLangTitle
                }).ToList(),
                pageId, pageCount);
        }
    }
}
