using FashionHouse.Data.DbModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<ProductAttributesEntity> ProductAttributesEntities { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<AttributesValuesProductEntity> AttributesValuesProductEntities { get; set; }

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
            });

            modelBuilder.Entity<ProductAttribute>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).HasMaxLength(1000);
                builder.Property(x => x.Description).HasMaxLength(1000);
            });

            modelBuilder.Entity<AttributesValuesProductEntity>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey(fk => fk.ProductId)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
                builder.HasOne<ProductAttribute>()
                    .WithMany()
                    .HasForeignKey(fk => fk.ProductAttributeId)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
                builder.HasOne<ProductAttributeValue>()
                    .WithMany()
                    .HasForeignKey(fk => fk.ProductAttributeValueId)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductAttributesEntity>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey(fk => fk.ProductEntityId)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
                builder.HasOne<ProductAttribute>()
                    .WithMany()
                    .HasForeignKey(fk => fk.ProductAttributeEntityId)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductImage>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey(fk => fk.ProductId)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductAttributeValue>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.AttributeValue).HasMaxLength(100);
                builder.HasOne<ProductAttribute>()
                    .WithMany()
                    .HasForeignKey(fk => fk.ProductAttributeId)
                    .HasPrincipalKey(pk => pk.Id)
                    .OnDelete(DeleteBehavior.Restrict);
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
