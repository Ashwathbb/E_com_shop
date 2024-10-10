using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.DTOs
{
    public class AuthResponseDto
    {
        public string? Token { get; set; }
        public string? UserName { get; set; }
    }
}
