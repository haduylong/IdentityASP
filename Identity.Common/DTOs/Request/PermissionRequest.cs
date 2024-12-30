using Identity.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.DTOs.Request
{
    public class PermissionRequest
    {
        public string PermissionId { get; set; }

        public string Description { get; set; }
    }
}
