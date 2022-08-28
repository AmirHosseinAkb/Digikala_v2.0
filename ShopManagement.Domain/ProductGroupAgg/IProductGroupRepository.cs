using System.Text.RegularExpressions;

namespace ShopManagement.Domain.ProductGroupAgg
{
    public interface IProductGroupRepository
    {
        List<ProductGroup> GetAll();
        List<ProductGroup> GetProductGroups(long? groupId);
        List<ProductGroup> GetProductGroupsForShow(string title = "");
        void Add(ProductGroup group);
        bool IsExistGroup(string title);
        ProductGroup? GetGroupById(long id);
        void SaveChanges();
        void Delete(ProductGroup group);
        List<ProductGroup> GetSubGroups(long groupId);


        List<GroupDetail> GetGroupDetails(long groupId);
        long AddGroupDetail(GroupDetail detail);
        bool IsExistGroupDetail(string detailTitle, long groupId);
        bool IsExistGroupDetail(long detailId, long groupId);
        GroupDetail GetGroupDetail(long detailId);
    }
}
