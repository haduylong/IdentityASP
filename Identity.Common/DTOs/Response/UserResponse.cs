using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.DTOs.Response
{
    #region --UserResponse--
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }

        //public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<RoleResponseBase> Roles { get; set; }
    }
    #endregion
}
