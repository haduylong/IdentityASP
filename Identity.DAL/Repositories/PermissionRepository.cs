using Identity.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Repositories
{
    public class PermissionRepository : GenericRepository<Permission, string>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context) : base(context)
        {
        }

    }
}
