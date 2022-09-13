﻿using DigikalaQuery.Contracts.Product;
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
        public Tuple<List<ProductBoxQueryModel>, List<ProductColorQueryModel>, List<ProductBrandQueryModel>, int, int,int> GetProductsForShow
        (SearchProductQueryModel searchModel)
        {
            var products = _context.Products.OrderByDescending(p=>p.CreationDate)
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

            int take = 1;

            int skip = (searchModel.PageId - 1) * take;

            var pageCount = products.Count() / take;

            if (products.Count() % take != 0)
                pageCount += 1;

            var productColors = products.SelectMany(p => p.ProductColors)
                .GroupBy(c => c.ColorName).Select(c => c.First()).ToList();

            var productBrands = products.Select(p => p.ProductBrand)
                .GroupBy(b => b.BrandTitle).Select(b => b.First()).ToList();

            var maxPrice = 0;
            if(products.Any())
                maxPrice=products.Max(p => p.Price);

            return Tuple.Create(
                products.Skip(skip).Take(take).Select(p => new ProductBoxQueryModel()
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
                productBrands.Distinct().Select(b => new ProductBrandQueryModel()
                {
                    BrandId = b.BrandId,
                    BrandTitle = b.BrandTitle,
                    OtherLangTitle = b.OtherLangTitle
                }).ToList(),
                searchModel.PageId, pageCount,maxPrice);
        }

        public Tuple<List<ProductBoxQueryModel>, int, int> GetProductsList(SearchProductQueryModel searchModel)
        {
            var products = _context.Products.OrderByDescending(p=>p.CreationDate)
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
            int take = 1;

            int skip = (searchModel.PageId - 1) * take;

            var pageCount = products.Count() / take;

            if (products.Count() % take != 0)
                pageCount += 1;

            return Tuple.Create(
                products
                    .Skip(skip)
                    .Take(take)
                    .Select(p => new ProductBoxQueryModel()
                    {
                        ProductId = p.ProductId,
                        Title = p.Title,
                        ImageName = p.ImageName,
                        Price = p.Price,
                        ProductColors = p.ProductColors
                    }).ToList(), searchModel.PageId, pageCount);
        }
    }
}
