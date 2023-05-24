﻿using Microsoft.EntityFrameworkCore;
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

            entity.Property(ua => ua.IsActive);

            entity.HasOne(ua => ua.User)
                  .WithMany(u => u.UserAccounts)
                  .HasForeignKey(ua => ua.UserId);

            entity.HasData(
                 new UserAccount { UserName = "phat.vo", Password = "123456", IsActive = true, UserId = 1 },
                 new UserAccount { UserName = "hai.tran", Password = "123456", IsActive = true, UserId = 2 },
                 new UserAccount { UserName = "admin", Password = "Rozitek123@#", IsActive = true, UserId = 1 }
            );

            base.Configure(entity);
        }

        #endregion
    }
}