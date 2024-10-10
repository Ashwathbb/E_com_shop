using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.DTOs
{
    public class APIResponseDto
    {
        public string? Message { get; set; }
        public bool Result { get; set; }
        public List<ProductDto1>? Data { get; set; }
    }
}
