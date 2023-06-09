﻿using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class UserAccount : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
