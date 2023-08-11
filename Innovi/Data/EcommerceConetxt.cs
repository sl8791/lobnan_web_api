using System;
using System.Collections.Generic;
using Innovi.Entities;
using Microsoft.EntityFrameworkCore;
using Attribute = Innovi.Entities.Attribute;

namespace Innovi.Data;

public partial class EcommerceConetxt : DbContext
{
    public EcommerceConetxt()
    {
    }

    public EcommerceConetxt(DbContextOptions<EcommerceConetxt> options)
        : base(options)
    {
    }

    public virtual DbSet<Ad> Ads { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<AttributeValue> AttributeValues { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<DeliveryOption> DeliveryOptions { get; set; }

    public virtual DbSet<Governorate> Governorates { get; set; }

    public virtual DbSet<MainPageNotification> MainPageNotifications { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Merchant> Merchants { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<OrderTracking> OrderTrackings { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttributesValue> ProductAttributesValues { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductQuantity> ProductQuantities { get; set; }

    public virtual DbSet<ProductSpecse> ProductSpecses { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<RegisteredMerchant> RegisteredMerchants { get; set; }

    public virtual DbSet<RolesPage> RolesPages { get; set; }

    public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }

    public virtual DbSet<ShippingStatus> ShippingStatuses { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<Storage> Storages { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SwipeBanner> SwipeBanners { get; set; }

    public virtual DbSet<SysConfig> SysConfigs { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<UsefulLink> UsefulLinks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=167.114.156.227; Database=Ecommerce; user id=sa; password=saINNOVI+-1234; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.MerchantId, "IX_AspNetUsers_MerchantId");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Merchant).WithMany(p => p.AspNetUsers).HasForeignKey(d => d.MerchantId);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.HasIndex(e => e.MerchantId, "IX_Attributes_MerchantId");

            entity.Property(e => e.IsDefault)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.HasOne(d => d.Merchant).WithMany(p => p.Attributes).HasForeignKey(d => d.MerchantId);
        });

        modelBuilder.Entity<AttributeValue>(entity =>
        {
            entity.HasIndex(e => e.AttributeId, "IX_AttributeValues_AttributeId");

            entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeValues).HasForeignKey(d => d.AttributeId);
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasIndex(e => e.CityId, "IX_Branches_CityId");

            entity.HasIndex(e => e.MerchantId, "IX_Branches_MerchantId");

            entity.Property(e => e.IsForOnlineSelling)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.HasOne(d => d.City).WithMany(p => p.Branches).HasForeignKey(d => d.CityId);

            entity.HasOne(d => d.Merchant).WithMany(p => p.Branches)
                .HasForeignKey(d => d.MerchantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.ParentCategoryId, "IX_Categories_ParentCategoryId");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory).HasForeignKey(d => d.ParentCategoryId);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasIndex(e => e.CountryId, "IX_Cities_CountryId");

            entity.HasIndex(e => e.GovernorateId, "IX_Cities_GovernorateId");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Governorate).WithMany(p => p.Cities).HasForeignKey(d => d.GovernorateId);
        });

        modelBuilder.Entity<DeliveryOption>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.FreeDeliveryAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.ShowAsNote)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.ShowForCustomers)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
        });

        modelBuilder.Entity<Governorate>(entity =>
        {
            entity.HasIndex(e => e.CountryId, "IX_Governorates_CountryId");

            entity.HasOne(d => d.Country).WithMany(p => p.Governorates).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<Merchant>(entity =>
        {
            entity.HasIndex(e => e.CountryId, "IX_Merchants_CountryId");

            entity.Property(e => e.Email).HasDefaultValueSql("(N'')");

            entity.HasOne(d => d.Country).WithMany(p => p.Merchants).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CreatedBy, "IX_Orders_CreatedBy");

            entity.HasIndex(e => e.PaymentMethodId, "IX_Orders_PaymentMethodId");

            entity.HasIndex(e => e.ShippingAddressId, "IX_Orders_ShippingAddressId");

            entity.Property(e => e.PaymentMethodId).HasDefaultValueSql("((3))");
            entity.Property(e => e.ShippingFees).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ShippingAddress).WithMany(p => p.Orders).HasForeignKey(d => d.ShippingAddressId);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetail");

            entity.HasIndex(e => e.OrderId, "IX_OrderDetail_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_OrderDetail_ProductId");

            entity.Property(e => e.ItemPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAttributesAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<OrderTracking>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderTrackings_OrderId");

            entity.HasIndex(e => e.OrderStatusId, "IX_OrderTrackings_OrderStatusId");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderTrackings).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.OrderTrackings).HasForeignKey(d => d.OrderStatusId);
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.Property(e => e.Action).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Controller).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Url)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("URL");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.HasIndex(e => e.ManufacturerId, "IX_Products_ManufacturerId");

            entity.HasIndex(e => e.MerchantId, "IX_Products_MerchantId");

            entity.HasIndex(e => e.StorageId, "IX_Products_StorageId");

            entity.HasIndex(e => e.SupplierId, "IX_Products_SupplierId");

            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsAvailableForPickup)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductCode).HasMaxLength(15);
            entity.Property(e => e.SupplierProductCode).HasMaxLength(15);
            entity.Property(e => e.VideoUrl).HasColumnName("VideoURL");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products).HasForeignKey(d => d.ManufacturerId);

            entity.HasOne(d => d.Merchant).WithMany(p => p.Products).HasForeignKey(d => d.MerchantId);

            entity.HasOne(d => d.Storage).WithMany(p => p.Products).HasForeignKey(d => d.StorageId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products).HasForeignKey(d => d.SupplierId);

            entity.HasMany(d => d.Tags).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductTag",
                    r => r.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                    l => l.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                    j =>
                    {
                        j.HasKey("ProductId", "TagId");
                        j.ToTable("ProductTag");
                        j.HasIndex(new[] { "TagId" }, "IX_ProductTag_TagId");
                    });
        });

        modelBuilder.Entity<ProductAttributesValue>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.AttributeValueId });

            entity.HasIndex(e => e.AttributeId, "IX_ProductAttributesValues_AttributeId");

            entity.HasIndex(e => e.AttributeValueId, "IX_ProductAttributesValues_AttributeValueId");

            entity.Property(e => e.ExtraCost).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Attribute).WithMany(p => p.ProductAttributesValues).HasForeignKey(d => d.AttributeId);

            entity.HasOne(d => d.AttributeValue).WithMany(p => p.ProductAttributesValues)
                .HasForeignKey(d => d.AttributeValueId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributesValues)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductImages_ProductId");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<ProductQuantity>(entity =>
        {
            entity.HasIndex(e => e.BranchId, "IX_ProductQuantities_BranchId");

            entity.HasIndex(e => e.ProductId, "IX_ProductQuantities_ProductId");

            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Branch).WithMany(p => p.ProductQuantities).HasForeignKey(d => d.BranchId);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductQuantities)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductSpecse>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductSpecses_ProductId");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSpecses).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Promotions_CategoryId");

            entity.HasIndex(e => e.MerchantId, "IX_Promotions_MerchantId");

            entity.Property(e => e.DiscountValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MinimumAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PromotionCode).HasMaxLength(20);

            entity.HasOne(d => d.Category).WithMany(p => p.Promotions).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Merchant).WithMany(p => p.Promotions).HasForeignKey(d => d.MerchantId);
        });

        modelBuilder.Entity<RegisteredMerchant>(entity =>
        {
            entity.HasIndex(e => e.CityId, "IX_RegisteredMerchants_CityId");

            entity.HasIndex(e => e.CountryId, "IX_RegisteredMerchants_CountryId");

            entity.HasIndex(e => e.GovernorateId, "IX_RegisteredMerchants_GovernorateId");

            entity.Property(e => e.CategoriesOfWork).HasDefaultValueSql("(N'')");
            entity.Property(e => e.CompanyName).HasDefaultValueSql("(N'')");

            entity.HasOne(d => d.City).WithMany(p => p.RegisteredMerchants)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Country).WithMany(p => p.RegisteredMerchants)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Governorate).WithMany(p => p.RegisteredMerchants)
                .HasForeignKey(d => d.GovernorateId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<RolesPage>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PageId });

            entity.HasIndex(e => e.PageId, "IX_RolesPages_PageId");

            entity.Property(e => e.HasAuthorization)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.HasOne(d => d.Page).WithMany(p => p.RolesPages).HasForeignKey(d => d.PageId);

            entity.HasOne(d => d.Role).WithMany(p => p.RolesPages).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<ShippingAddress>(entity =>
        {
            entity.HasIndex(e => e.CityId, "IX_ShippingAddresses_CityId");

            entity.HasIndex(e => e.CountryId, "IX_ShippingAddresses_CountryId");

            entity.HasIndex(e => e.GovernorateId, "IX_ShippingAddresses_GovernorateId");

            entity.HasIndex(e => e.UserId, "IX_ShippingAddresses_UserId");

            entity.Property(e => e.Building).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Flat).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Floor).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Street).HasDefaultValueSql("(N'')");

            entity.HasOne(d => d.City).WithMany(p => p.ShippingAddresses).HasForeignKey(d => d.CityId);

            entity.HasOne(d => d.Country).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Governorate).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.GovernorateId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ShippingStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasIndex(e => e.BranchId, "IX_ShoppingCarts_BranchId");

            entity.HasIndex(e => e.ProductId, "IX_ShoppingCarts_ProductId");

            entity.HasIndex(e => e.UserId, "IX_ShoppingCarts_UserId");

            entity.Property(e => e.SelectedAttributeValueIds).HasColumnName("selectedAttributeValueIds");

            entity.HasOne(d => d.Branch).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCarts).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Storage>(entity =>
        {
            entity.HasIndex(e => e.MerchantId, "IX_Storages_MerchantId");

            entity.HasOne(d => d.Merchant).WithMany(p => p.Storages)
                .HasForeignKey(d => d.MerchantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SwipeBanner>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_SwipeBanners_CategoryId");

            entity.Property(e => e.ImageAr).HasDefaultValueSql("(N'')");
            entity.Property(e => e.ImageEn).HasDefaultValueSql("(N'')");

            entity.HasOne(d => d.Category).WithMany(p => p.SwipeBanners).HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
