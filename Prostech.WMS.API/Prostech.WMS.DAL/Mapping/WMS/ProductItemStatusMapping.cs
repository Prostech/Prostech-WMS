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
    public class ProductItemStatusMapping : EntityTypeConfiguration<ProductItemStatus>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ProductItemStatus> entity)
        {
            entity.ToTable(nameof(ProductItemStatus), "public");

            entity.HasKey(pis => pis.ProductItemStatusId);

            entity.Property(pis => pis.ProductItemStatusName);

            entity.Property(c => c.IsActive).IsRequired();

            entity.Property(c => c.CreatedBy);

            entity.Property(c => c.CreatedTime);

            entity.Property(c => c.ModifiedBy);

            entity.Property(c => c.ModifiedTime);

            entity.Property(_ => _.GUID).HasDefaultValueSql("gen_random_uuid()");

            entity.HasMany(pis => pis.ProductItems)
                  .WithOne(pi => pi.ProductItemStatus)
                  .HasForeignKey(pi => pi.ProductItemStatusId);

            entity.HasData(
                 new ProductItemStatus { ProductItemStatusId = 1, ProductItemStatusName = "New", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ProductItemStatus { ProductItemStatusId = 2, ProductItemStatusName = "Second-hand", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
