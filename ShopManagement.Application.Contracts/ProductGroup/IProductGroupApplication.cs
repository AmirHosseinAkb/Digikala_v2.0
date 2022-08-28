using System.Net;
using _01_Framework.Application;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopManagement.Application.Contracts.ProductGroup
{
    public interface IProductGroupApplication
    {
        bool IsExistAnyGroup();
        List<SelectListItem> GetGroups(long? groupId);
        OperationResult CreateGroup(CreateGroupCommand command);
        
        Tuple<List<ProductGroupViewModel> , int, int, int> GetProductGroupsForShow(int pageId = 1, string title = "",
            int take = 10);
        EditGroupCommand GetGroupForEdit(long groupId);
        OperationResult EditGroup(EditGroupCommand command);
        DeleteGroupCommand GetGroupForDelete(long groupId);
        OperationResult DeleteGroup(long groupId);

        #region GroupDetail

        List<GroupDetailViewModel> GetDetailsOfGroup(long groupId);
        OperationResult CreateGroupDetail(CreateGroupDetailCommand command);
        OperationResult EditGroupDetail(CreateGroupDetailCommand command);
        
        #endregion
    }
}
