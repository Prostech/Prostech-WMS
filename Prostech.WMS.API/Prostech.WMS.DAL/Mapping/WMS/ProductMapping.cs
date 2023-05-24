using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prostech.WMS.DAL.Base;
using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Mapping.WMS
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable(nameof(Product), "public");

            entity.HasKey(p => p.ProductId);

            entity.Property(p => p.ProductName);
            entity.Property(p => p.Description);

            entity.HasOne(p => p.Brand)
                  .WithMany(b => b.Products)
                  .HasForeignKey(p => p.BrandId)
                  .IsRequired();

            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId)
                  .IsRequired();

            entity.HasMany(p => p.ProductItems)
                  .WithOne(pi => pi.Product)
                  .HasForeignKey(p => p.ProductId);

            entity.HasData(
                 new Product { ProductId = 1, ProductName = "Iphone 14" , Description = "Mô tả", BrandId = 1, CategoryId = 1},
                 new Product { ProductId = 2, ProductName = "Samsung Galaxy S23", Description = "Mô tả", BrandId = 2, CategoryId = 1 },
                 new Product { ProductId = 3, ProductName = "Asus ROG", Description = "Mô tả", BrandId = 3, CategoryId = 2 },
                 new Product { ProductId = 4, ProductName = "Airpod", Description = "Mô tả", BrandId = 1, CategoryId = 3 },
                 new Product { ProductId = 5, ProductName = "Xiaomi Plus 9", Description = "Mô tả", BrandId = 4, CategoryId = 1 }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
