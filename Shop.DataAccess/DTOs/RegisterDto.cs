using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.DTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
    }
}
