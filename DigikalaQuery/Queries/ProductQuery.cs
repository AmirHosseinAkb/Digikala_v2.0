﻿using System;
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
        (SearchProductQueryModel searchModel)
        {
            var products = _context.Products
                .Include(p => p.Inventory)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductBrand)
                .AsQueryable().
                AsNoTracking();

            if (!string.IsNullOrEmpty(searchModel.Title))
            {
                products = products.Where(p => p.Title.Contains(searchModel.Title));
            }

            switch (searchModel.OrderBy)
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
                foreach (var colorId in searchModel.Colors)
                {
                    products = products.Where(p => p.ProductColors.Any(c => c.ColorId == colorId));
                }
            }

            int take = 12;

            int skip = (searchModel.PageId - 1) * take;

            var pageCount = products.Count() / take;

            if (products.Count() % take != 0)
                pageCount += 1;

            var query = products.Skip(skip).Take(take);

            if (searchModel.StartPrice != 0)
                products = products.Where(p => p.Price > searchModel.StartPrice);
            if (query.Any())
            {
                if (searchModel.EndPrice != query.Max(p => p.Price))
                    products = products.Where(p => p.Price < searchModel.EndPrice);
            }


            var productColors = query.SelectMany(p=>p.ProductColors);


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
                productColors.Distinct().Select(c => new ProductColorQueryModel()
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
                searchModel.PageId, pageCount);
        }

        public List<ProductBoxQueryModel> GetProductsList(SearchProductQueryModel searchModel)
        {
            var products = _context.Products
                .Include(p => p.Inventory)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductBrand)
                .AsQueryable().
                AsNoTracking();

            if (!string.IsNullOrEmpty(searchModel.Title))
            {
                products = products.Where(p => p.Title.Contains(searchModel.Title)
                || p.OtherLangTitle.Contains(searchModel.Title)
                || p.Tags.Contains(searchModel.Title));
            }

            switch (searchModel.OrderBy)
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
                foreach (var colorId in searchModel.Colors)
                {
                    products = products.Where(p => p.ProductColors.Any(c => c.ColorId == colorId));
                }
            }

            int take = 12;

            int skip = (searchModel.PageId - 1) * take;

            var pageCount = products.Count() / take;

            if (products.Count() % take != 0)
                pageCount += 1;

            var query = products.Skip(skip).Take(take);

            if (searchModel.StartPrice != 0)
                products = products.Where(p => p.Price > searchModel.StartPrice);
            if (query.Any())
            {
                if (searchModel.EndPrice != query.Max(p => p.Price))
                    products = products.Where(p => p.Price < searchModel.EndPrice);
            }


            var productColors = _context.ProductColors.AsNoTracking().AsQueryable();
            foreach (var product in query)
            {
                productColors = productColors.Where(c => c.ProductId == product.ProductId);
            }

            var productBrands = query.Select(p => p.ProductBrand);

            return query.Select(p => new ProductBoxQueryModel()
            {
                ProductId = p.ProductId,
                Title = p.Title,
                ImageName = p.ImageName,
                Price = p.Price,
                ProductColors = p.ProductColors
            }).ToList();
        }
    }
}
