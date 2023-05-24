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

            entity.Property(b => b.BrandName);

            entity.HasMany(b => b.Products)
                  .WithOne(p => p.Brand)
                  .HasForeignKey(b => b.BrandId);

            entity.HasData(
                 new Brand { BrandId = 1, BrandName = "Apple"},
                 new Brand { BrandId = 2, BrandName = "Samsung" },
                 new Brand { BrandId = 3, BrandName = "Asus" },
                 new Brand { BrandId = 4, BrandName = "Xiaomi" },
                 new Brand { BrandId = 5, BrandName = "Sony" }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
