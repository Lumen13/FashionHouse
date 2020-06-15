using FashionHouse.Data.DbModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.EF
{
    public class MyContext : DbContext
    {
        public MyContext(string connectionString) : base(_getContextOptions(connectionString))
        {

        }

        private static DbContextOptions<MyContext> _getContextOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return optionsBuilder.Options;
        }        

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Seller> Sellers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).HasMaxLength(250);
                builder.Property(x => x.MarketingInfo).HasMaxLength(1000);
                builder.HasOne<Seller>()
                    .WithMany()
                    .HasForeignKey(fk => fk.SellerId)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
                builder.HasOne<ProductCategory>()
                    .WithMany()
                    .HasForeignKey(fk => fk.ProductCategoryId)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
                builder.HasOne<ProductAttribute>()
                    .WithMany()
                    .HasForeignKey(fk => fk.ProductAttributeId)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
                builder.Property(x => x.ProductCategoryId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<ProductAttribute>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).HasMaxLength(1000);
                builder.Property(x => x.Value).HasMaxLength(400);
                builder.Property(x => x.Description).HasMaxLength(1000);
            });

            modelBuilder.Entity<ProductCategory>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).HasMaxLength(200);
                builder.Property(x => x.Description).HasMaxLength(1000);
                builder.HasOne<ProductCategory>()
                    .WithMany()
                    .HasForeignKey(fk => fk.ParentId).IsRequired(false)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Seller>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).HasMaxLength(100);
                builder.Property(x => x.Email).HasMaxLength(100);
                builder.Property(x => x.Phone).HasMaxLength(50);
            });
        }
    }
}
