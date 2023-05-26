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
    public class ActionHistoryMapping : EntityTypeConfiguration<ActionHistory>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ActionHistory> entity)
        {
            entity.ToTable(nameof(ActionHistory), "public");

            entity.HasKey(_ => _.ActionHistoryId);

            entity.Property(_ => _.IsActive).IsRequired();

            entity.Property(_ => _.CreatedBy);

            entity.Property(_ => _.CreatedTime);

            entity.Property(_ => _.ModifiedBy);

            entity.Property(_ => _.ModifiedTime);

            entity.Property(_ => _.GUID).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(_ => _.ActionType)
                  .WithMany(_ => _.ActionHistories)
                  .HasForeignKey(_ => _.ActionTypeId);

            entity.HasMany(_ => _.ActionHistoryDetails)
                  .WithOne(_ => _.ActionHistory)
                  .HasForeignKey(_ => _.ActionHistoryId);

            entity.HasData(
                 new ActionHistory { ActionHistoryId = 1, ActionTypeId = 1, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
