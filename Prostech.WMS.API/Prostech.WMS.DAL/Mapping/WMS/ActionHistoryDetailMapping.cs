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

            entity.HasKey(_ => new {_.SKU, _.ActionHistoryId});

            entity.Property(_ => _.IsActive).IsRequired();

            entity.Property(_ => _.CreatedBy);

            entity.Property(_ => _.CreatedTime);

            entity.Property(_ => _.ModifiedBy);

            entity.Property(_ => _.ModifiedTime);

            entity.Property(_ => _.GUID).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(_ => _.ProductItem)
                  .WithMany(_ => _.ActionHistoryDetails)
                  .HasForeignKey(_ => _.SKU);

            entity.HasOne(_ => _.ActionHistory)
                  .WithMany(_ => _.ActionHistoryDetails)
                  .HasForeignKey(_ => _.ActionHistoryId);

            entity.HasData(
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 1, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 2, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 3, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 4, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 5, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 6, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 7, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 8, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 9, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 10, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 11, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 12, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 13, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 14, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 15, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 16, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 17, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 18, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 19, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 1, SKU = 20, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new ActionHistoryDetail { ActionHistoryId = 2, SKU = 19, IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow.AddHours(1) }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
