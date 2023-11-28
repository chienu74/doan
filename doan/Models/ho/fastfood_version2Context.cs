using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace doan.Models.ho
{
    public partial class fastfood_version2Context : DbContext
    {
        public fastfood_version2Context()
        {
        }

        public fastfood_version2Context(DbContextOptions<fastfood_version2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAccount> TbAccounts { get; set; } = null!;
        public virtual DbSet<TbAdminMenu> TbAdminMenus { get; set; } = null!;
        public virtual DbSet<TbBlog> TbBlogs { get; set; } = null!;
        public virtual DbSet<TbBlogComment> TbBlogComments { get; set; } = null!;
        public virtual DbSet<TbCategory> TbCategories { get; set; } = null!;
        public virtual DbSet<TbContact> TbContacts { get; set; } = null!;
        public virtual DbSet<TbCustomer> TbCustomers { get; set; } = null!;
        public virtual DbSet<TbDiscount> TbDiscounts { get; set; } = null!;
        public virtual DbSet<TbLocation> TbLocations { get; set; } = null!;
        public virtual DbSet<TbMenu> TbMenus { get; set; } = null!;
        public virtual DbSet<TbOrder> TbOrders { get; set; } = null!;
        public virtual DbSet<TbOrderDetail> TbOrderDetails { get; set; } = null!;
        public virtual DbSet<TbOrderStatus> TbOrderStatuses { get; set; } = null!;
        public virtual DbSet<TbProduct> TbProducts { get; set; } = null!;
        public virtual DbSet<TbProductCategory> TbProductCategories { get; set; } = null!;
        public virtual DbSet<TbProductReview> TbProductReviews { get; set; } = null!;
        public virtual DbSet<TbRole> TbRoles { get; set; } = null!;
        public virtual DbSet<ViewPostMenu> ViewPostMenus { get; set; } = null!;

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=CHIENNE;Initial Catalog=fastfood_version2;Integrated Security=True");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<TbAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("tb_Account");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.LastLogin)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TbAccounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_tb_Account_tb_Role");
            });

            modelBuilder.Entity<TbAdminMenu>(entity =>
            {
                entity.HasKey(e => e.AdminMenuId);

                entity.ToTable("tb_AdminMenu");

                entity.Property(e => e.ActionName).HasMaxLength(20);

                entity.Property(e => e.AreaName).HasMaxLength(20);

                entity.Property(e => e.ControllerName).HasMaxLength(20);

                entity.Property(e => e.Icon).HasMaxLength(50);

                entity.Property(e => e.IdName).HasMaxLength(50);

                entity.Property(e => e.ItemName).HasMaxLength(150);

                entity.Property(e => e.ItemTarget).HasMaxLength(20);
            });

            modelBuilder.Entity<TbBlog>(entity =>
            {
                entity.HasKey(e => e.BlogId)
                    .HasName("PK_tb_Post");

                entity.ToTable("tb_Blog");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("(N'Admin')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SeoDescription).HasMaxLength(500);

                entity.Property(e => e.SeoKeywords).HasMaxLength(250);

                entity.Property(e => e.SeoTitle).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TbBlogs)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_tb_Post_tb_Account");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TbBlogs)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_tb_Post_tb_Category");
            });

            modelBuilder.Entity<TbBlogComment>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("tb_BlogComment");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Detail).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.TbBlogComments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_tb_BlogComment_tb_Blog");
            });

            modelBuilder.Entity<TbCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK_tb_Menu");

                entity.ToTable("tb_Category");

                entity.Property(e => e.Alias).HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SeoDescription).HasMaxLength(500);

                entity.Property(e => e.SeoKeywords).HasMaxLength(250);

                entity.Property(e => e.SeoTitle).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(150);
            });

            modelBuilder.Entity<TbContact>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("tb_Contact");

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<TbCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("tb_Customer");

                entity.Property(e => e.Avatar).HasMaxLength(50);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TbCustomers)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_tb_Customer_tb_Account");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TbCustomers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_tb_Customer_tb_Location");
            });

            modelBuilder.Entity<TbDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountsId);

                entity.ToTable("tb_Discounts");

                entity.Property(e => e.DiscountsId).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbDiscounts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_tb_Discounts_tb_Product");
            });

            modelBuilder.Entity<TbLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("tb_Location");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TbMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK_tb_Menu_1");

                entity.ToTable("tb_Menu");

                entity.Property(e => e.Alias).HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Levels).HasDefaultValueSql("((1))");

                entity.Property(e => e.Link).HasMaxLength(150);

                entity.Property(e => e.MenuName).HasMaxLength(150);

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("tb_Order");

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TbOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_tb_Order_tb_Customer");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.TbOrders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .HasConstraintName("FK_tb_Order_tb_OrderStatus");
            });

            modelBuilder.Entity<TbOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId);

                entity.ToTable("tb_OrderDetail");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Discounts)
                    .WithMany(p => p.TbOrderDetails)
                    .HasForeignKey(d => d.DiscountsId)
                    .HasConstraintName("FK_tb_OrderDetail_tb_Discounts");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TbOrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_tb_OrderDetail_tb_Order");
            });

            modelBuilder.Entity<TbOrderStatus>(entity =>
            {
                entity.HasKey(e => e.OrderStatusId);

                entity.ToTable("tb_OrderStatus");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TbProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tb_Product");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.HasOne(d => d.CategoryProduct)
                    .WithMany(p => p.TbProducts)
                    .HasForeignKey(d => d.CategoryProductId)
                    .HasConstraintName("FK_tb_Product_db_CategoryProduct");
            });

            modelBuilder.Entity<TbProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryProductId)
                    .HasName("PK_db_Category");

                entity.ToTable("tb_ProductCategory");

                entity.Property(e => e.Alias).HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Icon).HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(150);
            });

            modelBuilder.Entity<TbProductReview>(entity =>
            {
                entity.HasKey(e => e.ProductReviewId);

                entity.ToTable("tb_ProductReview");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Detail).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbProductReviews)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_tb_ProductReview_tb_Product");
            });

            modelBuilder.Entity<TbRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_Role");

                entity.ToTable("tb_Role");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<ViewPostMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_Post_Menu");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.MenuName).HasMaxLength(150);

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SeoDescription).HasMaxLength(500);

                entity.Property(e => e.SeoKeywords).HasMaxLength(250);

                entity.Property(e => e.SeoTitle).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
