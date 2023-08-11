using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class Order
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string? OrderNo { get; set; }

    public int? ShippingAddressId { get; set; }

    public decimal ShippingFees { get; set; }

    public int DeliveryOptionId { get; set; }

    public int PaymentMethodId { get; set; }

    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual ShippingAddress? ShippingAddress { get; set; }
}
