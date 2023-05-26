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
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable(nameof(Category), "public");

            entity.HasKey(c => c.CategoryId);

            entity.Property(c => c.CategoryName);

            entity.Property(c => c.IsActive).IsRequired();

            entity.Property(c => c.CreatedBy);

            entity.Property(c => c.CreatedTime);

            entity.Property(c => c.ModifiedBy);

            entity.Property(c => c.ModifiedTime);

            entity.Property(_ => _.GUID).HasDefaultValueSql("gen_random_uuid()");

            entity.HasMany(c => c.Products)
                  .WithOne(p => p.Category)
                  .HasForeignKey(c => c.CategoryId);

            entity.HasData(
                 new Category { CategoryId = 1, CategoryName = "Laptop", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new Category { CategoryId = 2, CategoryName = "Smart phone", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new Category { CategoryId = 3, CategoryName = "Earbuds", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
