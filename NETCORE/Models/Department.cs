using Microsoft.EntityFrameworkCore.Infrastructure;
using NETCORE.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Models
{
    [Table("tb_m_department")]
    public class Department : BaseModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public DateTimeOffset DeletedDate { get; set; }
        public bool isDelete { get; set; }

        public virtual ICollection<Division> Divisions{ get; set; }
    }
}
