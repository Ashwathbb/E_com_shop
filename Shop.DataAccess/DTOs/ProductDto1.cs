using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.DTOs
{
    public class ProductDto1
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? ProductSku { get; set; }
        public string? ProductShortName { get; set; }
        public string? ProductDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? DeliveryTimeSpan { get; set; }
        public int CategoryId { get; set; }
        public string? ProductImageUrl { get; set; }
        public string? CategoryName { get; set; }
    }
}
