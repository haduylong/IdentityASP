using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.DTOs.Response
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public bool isAuthenticated { get; set; }
    }
}
