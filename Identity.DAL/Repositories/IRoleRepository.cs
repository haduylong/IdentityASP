using Identity.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Repositories
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<Role>> GetAllRoleAsync();
    }
}
