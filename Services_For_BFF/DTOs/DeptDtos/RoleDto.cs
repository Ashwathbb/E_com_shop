namespace Shop_BFF.DTOs.DeptDtos
{
    public class RoleDto
    {
        public int RoleId { get; set; }
        public Guid RoleGuid { get; set; }
        public int DepartmentId { get; set; }
        public string? RoleName { get; set; }
    }
}
