using _01_Framework.Application;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductGroup;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductGroupAgg;

namespace ShopManagement.Application
{
    public class ProductGroupApplication:IProductGroupApplication
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IProductRepository _productRepository;

        public ProductGroupApplication(IProductGroupRepository productGroupRepository,IProductRepository productRepository)
        {
            _productGroupRepository = productGroupRepository;
            _productRepository=productRepository;
        }

        public bool IsExistAnyGroup()
        {
            if (_productGroupRepository.GetAll().Count == 0)
                return false;
            return true;
        }

        public List<SelectListItem> GetGroups(long? groupId)
        {
            return _productGroupRepository.GetProductGroups(groupId).Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString()
            }).ToList();
        }


        public OperationResult CreateGroup(CreateGroupCommand command)
        {
            var result = new OperationResult();
            if (_productGroupRepository.IsExistGroup(command.Title))
                return result.Failed(ApplicationMessages.DuplicatedGroup);
            string? imageName = null;
            if (command.ParentId == null)
                imageName = DefaultImages.DefaultProductGroupImage;
            if (command.GroupImage != null)
            {
                imageName = CodeGenerator.GenerateUniqName() + Path.GetExtension(command.GroupImage.FileName);
                string imagePath = Directories.ProductGroupDirectory(imageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                    command.GroupImage.CopyTo(stream);
            }

            var group = new ProductGroup(command.Title, command.ParentId, imageName);
            _productGroupRepository.Add(group);
            return result.Succeeded();
        }

        public Tuple<List<ProductGroupViewModel> , int, int, int> GetProductGroupsForShow(int pageId = 1, string title = "", int take = 10)
        {
            var skip = (pageId - 1) * take;
            var groups = _productGroupRepository.GetProductGroupsForShow(title);
            var list = groups.Where(g => g.ParentId == null).Skip(skip).Take(take).ToList();

            foreach (var group in list.ToList())
            {
                var primaryGroups = groups.Where(g => g.ParentId == group.GroupId);
                list.AddRange(primaryGroups);
                foreach (var primaryGroup in primaryGroups)
                {
                    var secondaryGroups = groups.Where(g => g.ParentId == primaryGroup.GroupId);
                    list.AddRange(secondaryGroups);
                }
            }

            var groupCount = list.Where(g => g.ParentId == null).Count();
            var pageCount = groupCount / take;
            if (groupCount % take != 0)
                pageCount++;

            var finallGroups=list.Select(g => new ProductGroupViewModel()
            {
                GroupTitle = g.GroupTitle,
                ImageName =g.ImageName,
                GroupId = g.GroupId,
                ParentId =g.ParentId,
                
            }).ToList();
            return Tuple.Create(finallGroups, pageId, pageCount, take);
        }

        public EditGroupCommand GetGroupForEdit(long groupId)
        {
            var group = _productGroupRepository.GetGroupById(groupId);
            if (group == null)
                return new EditGroupCommand();

            return new EditGroupCommand()
            {
                Title = group.GroupTitle,
                ParentId = group.ParentId,
                GroupId = group.GroupId,
                ImageName = group.ImageName
            };
        }

        public OperationResult EditGroup(EditGroupCommand command)
        {
            var result = new OperationResult();
            var group = _productGroupRepository.GetGroupById(command.GroupId);
            if (group == null)
                return result.NullResult();
            if (group.GroupTitle != command.Title && command.ParentId==null)
            {
                if (_productGroupRepository.IsExistGroup(command.Title))
                    return result.Failed(ApplicationMessages.DuplicatedGroup);
            }

            string? imageName = command.ImageName;
            if (command.GroupImage != null)
            {
                string imagePath = "";
                if (command.ImageName != DefaultImages.DefaultProductGroupImage)
                {
                    imagePath = Directories.ProductGroupDirectory(command.ImageName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                imageName = CodeGenerator.GenerateUniqName() + Path.GetExtension(command.GroupImage.FileName);
                imagePath = Directories.ProductGroupDirectory(imageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                    command.GroupImage.CopyTo(stream);
            }

            group.Edit(command.Title, command.ParentId, imageName);
            _productGroupRepository.SaveChanges();
            return result.Succeeded();
        }

        public DeleteGroupCommand GetGroupForDelete(long groupId)
        {
            var group = _productGroupRepository.GetGroupById(groupId);
            if (group == null)
                return new DeleteGroupCommand();
            return new DeleteGroupCommand()
            {
                GroupTitle = group.GroupTitle,
                ImageName = group.ImageName,
                GroupId = group.GroupId,
                ParentId = group.ParentId
            };
        }

        public OperationResult DeleteGroup(long groupId)
        {
            var result = new OperationResult();
            var group = _productGroupRepository.GetGroupById(groupId);
            if (group == null)
                return result.NullResult();
            _productGroupRepository.Delete(group);
            return result.Succeeded();
        }

        public List<GroupDetailViewModel> GetDetailsOfGroup(long groupId)
        {
            return _productGroupRepository.GetGroupDetails(groupId)
                .Select(d=>new GroupDetailViewModel()
                {
                    DetailId = d.DetailId,
                    GroupId = d.GroupId,
                    DetailTitle = d.DetailTitle
                }).ToList();
        }

        public OperationResult CreateGroupDetail(CreateGroupDetailCommand command)
        {
            var result = new OperationResult();
            if (_productGroupRepository.IsExistGroupDetail(command.DetailTitle, command.GroupId))
                return result.Failed(ApplicationMessages.DuplicatedGroupDetail);
            var detail =new GroupDetail(command.GroupId, command.DetailTitle);
            _productGroupRepository.AddGroupDetail(detail);
            _productRepository.AddProductDetails(detail);
            return result.Succeeded();
        }

        public OperationResult EditGroupDetail(CreateGroupDetailCommand command)
        {
            var result = new OperationResult();
            var detail = _productGroupRepository.GetGroupDetail(command.DetailId!.Value);

            if (!_productGroupRepository.IsExistGroupDetail(command.DetailId!.Value, command.GroupId))
                return result.Failed(ApplicationMessages.RecordNotFound);

            if (command.DetailTitle != detail.DetailTitle)
            {
                if (_productGroupRepository.IsExistGroupDetail(command.DetailTitle,command.GroupId))
                    return result.Failed(ApplicationMessages.DuplicatedGroupDetail);
            }

            detail.Edit(command.DetailTitle);
            _productGroupRepository.SaveChanges();
            return result.Succeeded();
        }
    }
}
