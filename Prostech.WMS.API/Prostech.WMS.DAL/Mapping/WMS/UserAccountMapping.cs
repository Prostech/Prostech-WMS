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
    public class UserAccountMapping : EntityTypeConfiguration<UserAccount>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<UserAccount> entity)
        {
            entity.ToTable(nameof(UserAccount), "public");

            entity.HasKey(ua => ua.UserName);

            entity.Property(ua => ua.Password);

            entity.Property(ua => ua.IsActive).IsRequired();

            entity.Property(c => c.CreatedBy);

            entity.Property(c => c.CreatedTime);

            entity.Property(c => c.ModifiedBy);

            entity.Property(c => c.ModifiedTime);

            entity.Property(_ => _.GUID).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(ua => ua.User)
                  .WithMany(u => u.UserAccounts)
                  .HasForeignKey(ua => ua.UserId);

            entity.HasData(
                 new UserAccount { UserName = "phat.vo", Password = "123456", IsActive = true, UserId = 1, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new UserAccount { UserName = "hai.tran", Password = "123456", IsActive = true, UserId = 2, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                 new UserAccount { UserName = "admin", Password = "Rozitek123@#", IsActive = true, UserId = 1, CreatedBy = 1, CreatedTime = DateTime.UtcNow }
            );

            base.Configure(entity);
        }

        #endregion
    }
}
