using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Entities
{
    [Table("role")]
    public class Role
    {
        [Key]
        [StringLength(100)]
        [Column("Tên vai trò")]
        public string RoleId { get; set; }

        [Column("Mô tả", TypeName = "ntext")]
        public string Description { get; set; } = string.Empty;

        public List<Permission> Permissions { get; set; } = [];
        [Ignore]
        public List<User> Users { get; set; } = [];
    }

    [Table("role-permission")]
    public class RolePermission
    {
        public string RoleId { get; set; }
        public string PermissionId { get; set; }
    }

    [Table("user-role")]
    public class UserRole
    {
        public Guid UserId { get; set; }
        public string RoleId { get; set;}
    }
}
