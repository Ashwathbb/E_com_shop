using System;
using System.Collections.Generic;

namespace Dept.DataAcess.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int? CustomerId { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public Guid? AddressGuid { get; set; }

    public virtual Customer? Customer { get; set; }
}
