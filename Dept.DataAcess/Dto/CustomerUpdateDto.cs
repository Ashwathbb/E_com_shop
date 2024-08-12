using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class CustomerUpdateDto
    {
        public int CustomerID { get; set; }
        public string? LastName { get; set; }  
        public string? Email { get; set; }
        
    }
}
