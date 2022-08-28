using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductGroupAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class ProductGroupRepository:IProductGroupRepository
    {
        private readonly ShopContext _context;

        public ProductGroupRepository(ShopContext context)
        {
            _context = context;
        }

        public List<ProductGroup> GetAll()
        {
            return _context.ProductGroups.ToList();
        }

        public List<ProductGroup> GetProductGroups(long? groupId)
        {
            return _context.ProductGroups.Where(g => g.ParentId==groupId).ToList();
        }

        public List<ProductGroup> GetProductGroupsForShow(string title = "")
        {
            IQueryable<ProductGroup> groups=_context.ProductGroups;
            if (!string.IsNullOrEmpty(title))
                groups = groups.Where(g => g.GroupTitle.Contains(title));
            return groups.ToList();
        }

        public void Add(ProductGroup group)
        {
            _context.ProductGroups.Add(group);
            _context.SaveChanges();
        }

        public bool IsExistGroup(string title)
        {
            return _context.ProductGroups.Any(g => g.ParentId==null &&g.GroupTitle == title);
        }

        public ProductGroup? GetGroupById(long id)
        {
            return _context.ProductGroups.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Delete(ProductGroup group)
        {
            var primaryGroups = _context.ProductGroups.Where(g => g.ParentId == group.GroupId).ToList();
            var secondaryGroups = new List<ProductGroup>();
            if (primaryGroups.Any())
            {
                foreach (var primaryGroup in primaryGroups)
                {
                    secondaryGroups.AddRange(_context.ProductGroups.Where(g=>g.ParentId==primaryGroup.GroupId));
                    _context.ProductGroups.Remove(primaryGroup);
                }
            }
            if (secondaryGroups.Any())
            {
                foreach (var secondaryGroup in secondaryGroups)
                {
                    _context.ProductGroups.Remove(secondaryGroup);
                }
            }
            _context.ProductGroups.Remove(group);
            _context.SaveChanges();
        }

        public List<ProductGroup> GetSubGroups(long groupId)
        {
            return _context.ProductGroups.Where(g => g.ParentId == groupId).ToList();
        }

        public List<GroupDetail> GetGroupDetails(long groupId)
        {
            return _context.GroupDetails.Where(d => d.GroupId == groupId).ToList();
        }

        public long AddGroupDetail(GroupDetail detail)
        {
            _context.GroupDetails.Add(detail);
            _context.SaveChanges();
            return detail.DetailId;
        }

        public bool IsExistGroupDetail(string detailTitle, long groupId)
        {
            return _context.GroupDetails.Any(g => g.GroupId == groupId && g.DetailTitle == detailTitle);
        }

        public bool IsExistGroupDetail(long detailId, long groupId)
        {
            return _context.GroupDetails.Any(d => d.DetailId == detailId && d.GroupId == groupId);
        }

        public GroupDetail GetGroupDetail(long detailId)
        {
            return _context.GroupDetails.Find(detailId);
        }

    }
}
