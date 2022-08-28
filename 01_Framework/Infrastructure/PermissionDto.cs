namespace _01_Framework.Infrastructure;

public class PermissionDto
{
    public string PermissionTitle { get; set; }
    public int PermissionCode { get; set; }

    public PermissionDto(string permissionTitle,int permissionCode)
    {
        PermissionTitle=permissionTitle;
        PermissionCode = permissionCode;
    }
}