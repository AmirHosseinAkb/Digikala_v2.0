namespace _01_Framework.Infrastructure
{
    public class NeedsPermissionAttribute:Attribute
    {
        public int PermissionCode { get; set; }

        public NeedsPermissionAttribute(int permissionCode)
        {
            PermissionCode = permissionCode;
        }
    }
}
