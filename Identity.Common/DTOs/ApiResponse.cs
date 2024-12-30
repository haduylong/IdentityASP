using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.DTOs
{
    public class ApiResponse<T>
    {
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
