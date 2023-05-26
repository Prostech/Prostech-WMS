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
    public class ActionHistoryDetailMapping : EntityTypeConfiguration<ActionHistoryDetail>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ActionHistoryDetail> entity)
        {
            entity.ToTable(nameof(ActionHistoryDetail), "public");

            entity.HasKey(_ => new {_.ProductId, _.ActionHistoryId});

            entity.Property(_ => _.IsActive).IsRequired();

            entity.Property(_ => _.CreatedBy);

            entity.Property(_ => _.CreatedTime);

            entity.Property(_ => _.ModifiedBy);

            entity.Property(_ => _.ModifiedTime);

            entity.Property(_ => _.GUID).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(_ => _.Product)
                  .WithMany(_ => _.ActionHistoryDetails)
                  .HasForeignKey(_ => _.ProductId);

            entity.HasOne(_ => _.ActionHistory)
                  .WithMany(_ => _.ActionHistoryDetails)
                  .HasForeignKey(_ => _.ActionHistoryId);

            entity.HasData(
                 new ActionHistoryDetail { ActionHistoryId = 1, ProductId = 1, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, ProductId = 2, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, ProductId = 3, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, ProductId = 4, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, ProductId = 5, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
