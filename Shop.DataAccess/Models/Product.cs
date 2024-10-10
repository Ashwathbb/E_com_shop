using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public decimal Price { get; set; }

    public Guid ProductGuid { get; set; }

    public string? ProductSku { get; set; }

    public string? ProductShortName { get; set; }

    public string? ProductDescription { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? DeliveryTimeSpan { get; set; }

    public int? CategoryId { get; set; }

    public string? ProductImageUrl { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<UsersInfo> Users { get; set; } = new List<UsersInfo>();
}
