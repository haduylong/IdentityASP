using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;

namespace Identity.DAL.Entities
{
    [Table("permission")]
    public class Permission
    {
        [Key]
        [StringLength(100)]
        [Column("Tên quyền")]
        public string PermissionId { get; set; }

        [Column("Mô tả", TypeName = "ntext")]
        public string Description { get; set; } = string.Empty;

        [Ignore]
        public List<Role> Roles { get; set; } = [];
    }
}
