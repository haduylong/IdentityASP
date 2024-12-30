using Identity.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role, string>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await _dbSet.Include(r => r.Permissions).ToListAsync();
        }
    }
}
