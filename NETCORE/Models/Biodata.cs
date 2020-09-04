using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Models
{
    [Table("tb_m_biodata")]
    public class Biodata
    {
        [ForeignKey("User")]
        public string Id { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string Address { get; set; }

        public virtual User User { get; set; }
    }
}
