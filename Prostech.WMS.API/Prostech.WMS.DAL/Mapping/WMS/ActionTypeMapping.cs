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
    public class ActionTypeMapping : EntityTypeConfiguration<ActionType>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ActionType> entity)
        {
            entity.ToTable(nameof(ActionType), "public");

            entity.HasKey(_ => _.ActionTypeId);

            entity.Property(_ => _.ActionName).IsRequired();

            entity.Property(_ => _.IsActive).IsRequired();

            entity.Property(_ => _.CreatedBy);

            entity.Property(_ => _.CreatedTime);

            entity.Property(_ => _.ModifiedBy);

            entity.Property(_ => _.ModifiedTime);

            entity.Property(_ => _.GUID).HasDefaultValueSql("gen_random_uuid()");

            entity.HasMany(_ => _.ActionHistories)
                  .WithOne(_ => _.ActionType)
                  .HasForeignKey(_ => _.ActionTypeId);

            entity.HasData(
                 new ActionType { ActionTypeId = 1, ActionName = "In bound", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionType { ActionTypeId = 2, ActionName = "Out bound", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
