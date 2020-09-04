using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Models
{
    [Table("tb_m_user")]
    public class User : IdentityUser
    {


        public virtual Biodata Biodata { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        
    }
}
