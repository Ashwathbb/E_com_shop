namespace Shop_BFF.DTOs.DeptDtos
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public Guid DepartmentGuid { get; set; }
        public string? DepartmentName { get; set; }

        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }
}
