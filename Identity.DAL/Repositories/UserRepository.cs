using Identity.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Repositories
{
    public class UserRepository : GenericRepository<User, Guid>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistByUsername(string username)
        {
            return await _dbSet.AnyAsync(user => user.Username == username);
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _dbSet.Include(user => user.Roles).ToListAsync();
        }

        /// <summary>
        /// Lấy ra các user kèm theo role tương ứng
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(params Expression<Func<User, bool>>[] filters)
        {
            IQueryable<User> query = _dbSet.Include(user => user.Roles);

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
