using ShopManagement.Domain.InventoryAgg;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductBrandAgg;
using ShopManagement.Domain.ProductColorAgg;
using ShopManagement.Domain.ProductGroupAgg;
using ShopManagement.Domain.ProductImageAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product
    {
        public long ProductId { get; private set; }
        public long BrandId { get;private set; }
        public long GroupId { get; private set; }
        public long? PrimaryGroupId { get; private set; }
        public long? SecondaryGroupId { get;private set; }
        public string Title { get; private set; }
        public string OtherLangTitle { get; private set; }
        public string Description { get;private set; }
        public int Price { get; private set; }
        public string Tags { get; private set; }
        public string ImageName { get;private set; }
        public DateTime CreationDate { get;private set; }
        public bool IsDeleted { get;private set; }

        public ProductBrand ProductBrand { get; set; }
        public ProductGroup ProductGroup { get;private set; }
        public ProductGroup PrimaryProductGroup { get;private set; }
        public ProductGroup SecondaryProductGroup { get; private set; }
        public List<ProductImage> ProductImages { get;private set; }
        public List<ProductColor> ProductColors { get;private set; }
        public List<ProductDetail> ProductDetails { get; private set; }
        public Inventory Inventory { get; private set; }
        public List<InventoryHistory> InventoryHistories { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }


        protected Product()
        {
            
        }

        public Product(long groupId,long brandId, long? primaryGroupId, long? secondaryGroupId, string title, string description, int price
            , string otherLangTitle, string tags, string imageName)
        {
            GroupId = groupId;
            BrandId = brandId;
            PrimaryGroupId = primaryGroupId;
            SecondaryGroupId = secondaryGroupId;
            Title = title;
            OtherLangTitle = otherLangTitle;
            Description = description;
            Price = price;
            Tags = tags;
            ImageName = imageName;
            CreationDate = DateTime.Now;
            IsDeleted = false;
        }
        public void Edit(long groupId,long brandId, long? primaryGroupId, long? secondaryGroupId, string title, string description, int price
            , string otherLangTitle, string tags, string imageName)
        {
            GroupId = groupId;
            BrandId = brandId;
            PrimaryGroupId = primaryGroupId;
            SecondaryGroupId = secondaryGroupId;
            Title = title;
            OtherLangTitle = otherLangTitle;
            Description = description;
            Price = price;
            Tags = tags;
            ImageName = imageName;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
