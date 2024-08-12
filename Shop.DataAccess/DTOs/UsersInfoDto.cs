namespace Shop.DataAccess.DTOs
{
    public class UsersInfoDto
    {
        public Guid UsersInfoGuid { get; set; }

        public Guid DepartmentGuid { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;

        public string? Password { get; set; }
        public bool IsActive { get; set; }

        public int FailedLoginAttempts { get; set; }


    }

    public class CreateUserDto
    {
        public string UserName { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public Guid DepartmentGuid { get; set; }

    }
   
}
