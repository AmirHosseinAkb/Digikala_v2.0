using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductGroupAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context)
        {
            _context = context;
        }

        public long AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.ProductId;
        }

        public Product GetProductById(long productId)
        {
            return _context.Products.Find(productId);
        }

        public Product GetProductWithGroups(long productId)
        {
            return _context.Products.Include(p => p.ProductGroup)
                .Include(p => p.PrimaryProductGroup)
                .Include(p => p.SecondaryProductGroup)
                .SingleOrDefault(p => p.ProductId == productId);
        }

        public List<Product> GetAll(string title = "", long groupId = 0, long primaryGroupId = 0, long secondaryGroupId = 0)
        {
            IQueryable<Product> products = _context.Products;

            if (!string.IsNullOrWhiteSpace(title))
                products = products.Where(p => p.Title.Contains(title));

            if (groupId != 0)
                products = products.Where(p => p.GroupId == groupId);

            if (primaryGroupId != 0)
                products = products.Where(p => p.PrimaryGroupId == primaryGroupId);

            if (secondaryGroupId != 0)
                products = products.Where(p => p.SecondaryGroupId == secondaryGroupId);

            return products.ToList();
        }

        public bool IsExistProduct(string title)
        {
            return _context.Products.Any(p => p.Title == title);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Product GetProductByGroupId(long groupId)
        {
            return _context.Products.SingleOrDefault(g => g.GroupId == groupId || g.PrimaryGroupId == groupId || g.SecondaryGroupId == groupId);
        }

        public List<Product> GetProductsByGroupId(long groupId)
        {
            return _context.Products.Where(g =>
                g.GroupId == groupId || g.PrimaryGroupId == groupId || g.SecondaryGroupId == groupId).ToList();
        }

        public void AddProductDetails(GroupDetail detail)
        {
            var products = GetProductsByGroupId(detail.GroupId);
            foreach (var product in products)
            {
                _context.ProductDetails.Add(new ProductDetail(product.ProductId,detail.DetailId,null));
            }

            _context.SaveChanges();
        }

        public void AddProductDetails(Product product)
        {
            var details = _context.GroupDetails.Where(g =>
                g.GroupId == product.GroupId || g.GroupId == product.PrimaryGroupId ||
                g.GroupId == product.SecondaryGroupId);
            foreach (var detail in details)
            {
                _context.ProductDetails.Add(new ProductDetail(product.ProductId, detail.DetailId, null));
            }

            _context.SaveChanges();
        }

        public void EditProductDetails(Product product)
        {
            _context.ProductDetails.Where(d=>d.ProductId==product.ProductId).ToList()
                .ForEach(d=>_context.ProductDetails.Remove(d));
            var details=_context.GroupDetails.Where(g =>
                g.GroupId == product.GroupId || g.GroupId == product.PrimaryGroupId ||
                g.GroupId == product.SecondaryGroupId);
            if (details.Any())
            {
                foreach (var detail in details)
                {
                    _context.ProductDetails.Add(new ProductDetail(product.ProductId, detail.DetailId, null));
                }
            }

            _context.SaveChanges();
        }

        public List<ProductDetail> GetProductDetails(long productId)
        {
            return _context.ProductDetails.Include(d=>d.GroupDetail).Where(d => d.ProductId == productId).ToList();
        }

        public void ConfirmProductDetails(long productId, Dictionary<int, string> details)
        {
            foreach (var (key,value) in details)
            {
                var productDetail =
                    _context.ProductDetails.SingleOrDefault(d => d.DetailId == key && d.ProductId == productId);
                productDetail.Edit(value);
            }
            _context.SaveChanges();
        }
    }
}
