using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Entities
{
    [Table("user")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        
        [Required]
        [StringLength(100)] // unique
        public string Username { get; set; }

        [StringLength(512, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Column("Ngày sinh")]
        public DateTime Dob { get; set; }

        [Column("Ngày tạo")]
        public DateTime CreatedAt { get; set; }

        public List<Role> Roles { get; set; } = [];
    }
}
