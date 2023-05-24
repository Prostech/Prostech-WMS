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

            entity.HasMany(c => c.Products)
                  .WithOne(p => p.Category)
                  .HasForeignKey(c => c.CategoryId);

            entity.HasData(
                 new Category { CategoryId = 1, CategoryName = "Laptop" },
                 new Category { CategoryId = 2, CategoryName = "Smart phone" },
                 new Category { CategoryId = 3, CategoryName = "Earbuds" }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
