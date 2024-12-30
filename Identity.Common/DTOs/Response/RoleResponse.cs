using Identity.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.DTOs.Response
{
    public class RoleResponseBase
    {
        public string RoleId { get; set; }
        public string Description { get; set; }
    }

    public class RoleResponse : RoleResponseBase
    {
        public List<PermissionResponse> Permissions { get; set; }
    }
}
