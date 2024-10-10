using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Models
{
    public class APIResponseModel
    {
        public string Message { get; set; }
        public bool Result { get; set; }
        public List<Product>? Data { get; set; }
    }
}
