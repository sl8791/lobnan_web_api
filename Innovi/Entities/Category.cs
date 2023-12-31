﻿using Innovi.Entities;
using Innovi.Models;



public partial class Category
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string Code { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public int? ParentCategoryId { get; set; }

    public bool AddToHomePage { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<SwipeBanner> SwipeBanners { get; set; } = new List<SwipeBanner>();

    public static implicit operator Category(CategoryDto v)
    {
        throw new NotImplementedException();
    }
}
