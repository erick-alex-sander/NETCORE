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
    [Table("tb_m_division")]
    public class Division : BaseModel
    {
        private readonly ILazyLoader _lazyLoader;

        public Division()
        {

        }
        public Division(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        private Department _department;

        public int Id { get ; set; }
        [Required]
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public DateTimeOffset DeletedDate { get; set; }
        public bool isDelete { get; set; }

        public virtual Department Department
        {
            get => _lazyLoader.Load(this, ref _department);
            set => _department = value;
        }
        
    }
}
