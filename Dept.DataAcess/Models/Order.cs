using System;
using System.Collections.Generic;

namespace Dept.DataAcess.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public Guid? OrderGuid { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
