using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            UserAccounts = new HashSet<UserAccount>();
        }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ? ModifiedTime { get; set; }
        public int? ModifiedBy { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set;}
    }
}
