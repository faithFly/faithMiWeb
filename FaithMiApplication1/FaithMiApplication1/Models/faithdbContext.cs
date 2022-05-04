using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FaithMiApplication1.Models
{
    public partial class faithdbContext : DbContext
    {
        public faithdbContext()
        {
        }

        public faithdbContext(DbContextOptions<faithdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carousel> Carousels { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersProduct> OrdersProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPicture> ProductPictures { get; set; }
        public virtual DbSet<Shoppingcart> Shoppingcarts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=112.124.10.13;user id=root;password=xyf12138;database=faithdb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Carousel>(entity =>
            {
                entity.ToTable("carousel");

                entity.Property(e => e.CarouselId).HasColumnName("carousel_id");

                entity.Property(e => e.Describes)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("describes")
                    .IsFixedLength(true);

                entity.Property(e => e.ImgPath)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("imgPath")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("category_name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .HasColumnName("id");

                entity.Property(e => e.BuyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("buy_time")
                    .HasComment("购买时间");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("订单状态");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户id");
            });

            modelBuilder.Entity<OrdersProduct>(entity =>
            {
                entity.ToTable("orders_product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BuyNum)
                    .HasColumnName("buy_num")
                    .HasComment("购买数量");

                entity.Property(e => e.Money)
                    .HasPrecision(10)
                    .HasColumnName("money")
                    .HasComment("购买单价");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(20)
                    .HasColumnName("order_id")
                    .HasComment("订单id");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasComment("商品id");
                entity.Property(e => e.UserId)
                    .HasMaxLength(20)
                    .HasColumnName("userid")
                    .HasComment("用户id");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.CategoryId, "FK_product_category");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ProductIntro)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("product_intro");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("product_name")
                    .IsFixedLength(true);

                entity.Property(e => e.ProductNum).HasColumnName("product_num");

                entity.Property(e => e.ProductPicture)
                    .HasMaxLength(200)
                    .HasColumnName("product_picture")
                    .IsFixedLength(true);

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.Property(e => e.ProductSales).HasColumnName("product_sales");

                entity.Property(e => e.ProductSellingPrice).HasColumnName("product_selling_price");

                entity.Property(e => e.ProductTitle)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("product_title")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_category");
            });

            modelBuilder.Entity<ProductPicture>(entity =>
            {
                entity.ToTable("product_picture");

                entity.HasIndex(e => e.ProductId, "FK_product_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Intro)
                    .HasColumnType("text")
                    .HasColumnName("intro");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductPicture1)
                    .HasMaxLength(200)
                    .HasColumnName("product_picture")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPictures)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_id");
            });

            modelBuilder.Entity<Shoppingcart>(entity =>
            {
                entity.ToTable("shoppingcart");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CartNum).HasColumnName("cart_num");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.UserName, "userName")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("userName")
                    .IsFixedLength(true);

                entity.Property(e => e.UserPhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("userPhoneNumber")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
