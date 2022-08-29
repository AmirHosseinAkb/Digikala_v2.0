using DigikalaQuery.Contracts.ProductGroup;
using ShopManagement.Infrastructure.EfCore;

namespace DigikalaQuery.Queries
{
    public class ProductGroupQuery:IProductGroupQuery
    {
        private readonly ShopContext _shopContext;

        public ProductGroupQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public List<ProductGroupQueryModel> GetProductGroups()
        {
            return _shopContext.ProductGroups.Select(g => new ProductGroupQueryModel()
            {
                GroupId = g.GroupId,
                ImageName = g.ImageName,
                GroupTitle = g.GroupTitle,
                ParentId = g.ParentId
            }).ToList();
        }
    }
}
