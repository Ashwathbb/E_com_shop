using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.DTOs
{
    public class LinqOperationResultDto
    {
        public List<ProductDto>? ExpensiveProducts { get; set; }
        public List<ProductDto>? SpecificProduct { get; set; }
        public List<ProductDto1>? CategoryProducts { get; set; }
        public List<ProductDto>? Result1 { get; set; }
        public ProductDto? Result2 { get; set; }
        public List<ProductDto1>? Multiple_conditions { get; set ;}
        public List<ProductCartDto>? Product_cart_join { get; set; }
        public List<ProductCartDto>? JoinOperation { get; set; }

    }
}
