using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductColorAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class ProductColorRepository:IProductColorRepository
    {
        private readonly ShopContext _context;

        public ProductColorRepository(ShopContext context)
        {
            _context = context;
        }
        public bool IsExistColor(long productId, string colorName, string colorCode)
        {
            return _context.ProductColors.Any(c =>
                c.ProductId == productId && (c.ColorName == colorName || c.ColorCode == colorCode));
        }

        public void Add(ProductColor color)
        {
            _context.ProductColors.Add(color);
            _context.SaveChanges();
        }

        public List<ProductColor> GetAll()
        {
            return _context.ProductColors.ToList();
        }

        public ProductColor GetById(long colorId)
        {
            return _context.ProductColors.Find(colorId);
        }

        public void Delete(ProductColor color)
        {
            _context.ProductColors.Remove(color);
            _context.SaveChanges();
        }
    }
}
