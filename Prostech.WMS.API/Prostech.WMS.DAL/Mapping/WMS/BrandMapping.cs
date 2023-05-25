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
    public class BrandMapping : EntityTypeConfiguration<Brand>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Brand> entity)
        {
            entity.ToTable(nameof(Brand), "public");

            entity.HasKey(b => b.BrandId);

            entity.Property(b => b.BrandName).IsRequired();

            entity.Property(b => b.IsActive).IsRequired();

            entity.Property(b => b.CreatedBy);

            entity.Property(b => b.CreatedTime);

            entity.Property(b => b.ModifiedBy);

            entity.Property(b => b.ModifiedTime);


            entity.HasMany(b => b.Products)
                  .WithOne(p => p.Brand)
                  .HasForeignKey(b => b.BrandId);

            entity.HasData(
                 new Brand { BrandId = 1, BrandName = "Apple", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow}, 
                 new Brand { BrandId = 2, BrandName = "Samsung", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new Brand { BrandId = 3, BrandName = "Asus", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new Brand { BrandId = 4, BrandName = "Xiaomi", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new Brand { BrandId = 5, BrandName = "Sony", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
