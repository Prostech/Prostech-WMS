using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Prostech.WMS.DAL.Base;
using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Mapping.WMS
{
    public class ProductItemMapping : EntityTypeConfiguration<ProductItem>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ProductItem> entity)
        {
            entity.ToTable(nameof(ProductItem), "public");

            entity.HasKey(pi => pi.SKU);

            entity.Property(pi => pi.Price);

            entity.Property(c => c.IsStock).IsRequired();

            entity.Ignore(_ => _.IsActive);

            entity.Property(c => c.CreatedBy);

            entity.Property(c => c.CreatedTime);

            entity.Property(c => c.ModifiedBy);

            entity.Property(c => c.ModifiedTime);

            entity.Property(_ => _.GUID).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(pi => pi.Product)
                  .WithMany(p => p.ProductItems)
                  .HasForeignKey(pi => pi.ProductId)
                  .IsRequired();

            entity.HasOne(pi => pi.ProductItemStatus)
                  .WithMany(pis => pis.ProductItems)
                  .HasForeignKey(pi => pi.ProductItemStatusId)
                  .IsRequired();

            entity.HasData(
                 new ProductItem { SKU = 1, ProductId = 1, Price = 20000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 2, ProductId = 1, Price = 20000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 3, ProductId = 1, Price = 20000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 4, ProductId = 1, Price = 20000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 5, ProductId = 1, Price = 21000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 6, ProductId = 2, Price = 27000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 7, ProductId = 2, Price = 27000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 8, ProductId = 2, Price = 27000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 9, ProductId = 2, Price = 27000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 10, ProductId = 3, Price = 40000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 11, ProductId = 3, Price = 40000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 12, ProductId = 4, Price = 4000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 13, ProductId = 5, Price = 4000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 14, ProductId = 5, Price = 4000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 15, ProductId = 5, Price = 4000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 16, ProductId = 2, Price = 20000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 17, ProductId = 3, Price = 40000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 18, ProductId = 3, Price = 40000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 19, ProductId = 3, Price = 43000000, ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItem { SKU = 20, ProductId = 2, Price = 28000000 , ProductItemStatusId = 1, IsStock = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
