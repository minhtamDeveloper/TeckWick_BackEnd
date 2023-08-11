using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlantNestBackEnd.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<FavoriteCart> FavoriteCarts { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DNGUYXNTUANANH;Database=PlantNestDB;user id=sa;password=0335167226aA;trusted_connection=true;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__account__3213E83F00B0DDE1");
=======
            entity.HasKey(e => e.Id).HasName("PK__account__3213E83F6D8888EB");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountImage)
                .IsUnicode(false)
                .HasColumnName("account_image");
            entity.Property(e => e.Address)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.SercurityCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("sercurityCode");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_account_role");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__cart__3213E83FB7A884B6");
=======
            entity.HasKey(e => e.Id).HasName("PK__cart__3213E83F699F73CB");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("cart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Account).WithMany(p => p.Carts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("fk_acccount_cart");
        });

        modelBuilder.Entity<Category>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__category__3213E83F01300EFA");
=======
            entity.HasKey(e => e.Id).HasName("PK__category__3213E83F5C6D9503");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryImage)
                .IsUnicode(false)
                .HasColumnName("category_image");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("category_name");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.InverseCategoryNavigation)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_category_category");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__comment__3213E83F25ABCA18");
=======
            entity.HasKey(e => e.Id).HasName("PK__comment__3213E83FFA3C95C2");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("comment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Content)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_commnents_product");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__contact__3213E83F8D06CF42");
=======
            entity.HasKey(e => e.Id).HasName("PK__contact__3213E83FED0DA462");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("contact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Message)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Subject)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("subject");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__delivery__3213E83F79CF5C21");
=======
            entity.HasKey(e => e.Id).HasName("PK__delivery__3213E83F3D3FDC64");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("delivery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("delivery_date");
            entity.Property(e => e.Message)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ReceivingDate)
                .HasColumnType("date")
                .HasColumnName("receiving_date");
            entity.Property(e => e.RecipientAddress)
                .IsUnicode(false)
                .HasColumnName("recipient_address");
            entity.Property(e => e.RecipientName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("recipient_name");
            entity.Property(e => e.RecipientPhone)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("recipient_phone");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Order).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_order_delivery");
        });

        modelBuilder.Entity<FavoriteCart>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__favorite__3213E83FFFF41E57");
=======
            entity.HasKey(e => e.Id).HasName("PK__favorite__3213E83F5A0EBA27");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("favoriteCart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Account).WithMany(p => p.FavoriteCarts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("fk_acccount_favoriteCart");
        });

        modelBuilder.Entity<Image>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__image__3213E83F6EDF4317");
=======
            entity.HasKey(e => e.Id).HasName("PK__image__3213E83FF9AC8EA9");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("image");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("imageUrl");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_product_image");
        });

        modelBuilder.Entity<Order>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__order__3213E83FCA01C699");
=======
            entity.HasKey(e => e.Id).HasName("PK__order__3213E83FA8EE29F3");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("orderDate");
            entity.Property(e => e.OrderTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("orderTime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("payment_method");
            entity.Property(e => e.Status)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TotalOrder)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("total_order");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("fk_account_order");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__orderDet__022945F64976148A");
=======
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__orderDet__022945F6AB961A7C");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("orderDetail");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("total_price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_orderDetails");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_orderDetails");
        });

        modelBuilder.Entity<Product>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__product__3213E83FF9D86EBF");
=======
            entity.HasKey(e => e.Id).HasName("PK__product__3213E83F2F84D572");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CostPrice)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("cost_price");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SellPrice)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("sell_price");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_category_product");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("fk_supplier_product");
        });

        modelBuilder.Entity<Role>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83F69825B13");
=======
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83F1991A6BF");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__supplier__3213E83FED92903D");
=======
            entity.HasKey(e => e.Id).HasName("PK__supplier__3213E83F370017A3");
>>>>>>> 105dc2352866507a1ef1c9d019512ef0208de150

            entity.ToTable("supplier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("supplier_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
