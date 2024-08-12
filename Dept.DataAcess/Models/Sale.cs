using System;
using System.Collections.Generic;

namespace Dept.DataAcess.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Amount { get; set; }

    public Guid? SaleGuid { get; set; }

    public virtual Order? Order { get; set; }
}
