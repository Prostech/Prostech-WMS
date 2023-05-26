﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Base
{
    /// <summary>
    /// Represents the base class for DTO request entities
    /// </summary>
    public abstract partial class BaseRequestEntitty
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
