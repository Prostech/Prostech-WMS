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
    public class UserMapping : EntityTypeConfiguration<User>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable(nameof(User), "public");

            entity.HasKey(u => u.UserId);

            entity.Property(u => u.Fullname);

            entity.Property(u => u.Email);

            entity.Property(u => u.Phone);

            entity.Property(ua => ua.IsActive).IsRequired();

            entity.Property(c => c.CreatedBy);

            entity.Property(c => c.CreatedTime);

            entity.Property(c => c.ModifiedBy);

            entity.Property(c => c.ModifiedTime);

            entity.Property(_ => _.GUID).HasDefaultValueSql("gen_random_uuid()");

            entity.HasMany(u => u.UserAccounts)
                  .WithOne(ua => ua.User)
                  .HasForeignKey(u => u.UserId);

            entity.HasData(
                 new User { UserId = 1, Fullname = "Vo Tan Phat", Email = "phat.vo@rozitek.com", Phone = "123", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new User { UserId = 2, Fullname = "Tran Hung Hai", Email = "hai.tran@rozitek.com", Phone = "456", IsActive = true, CreatedBy = 1, CreatedTime = DateTime.UtcNow }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
