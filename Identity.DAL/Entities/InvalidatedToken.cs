using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Entities
{
    public class InvalidatedToken
    {
        [Key]
        public string Id { get; set; } // jwtId
        public DateTime ExpiryTime { get; set; }
    }
}
