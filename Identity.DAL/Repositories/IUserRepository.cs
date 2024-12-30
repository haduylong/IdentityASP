using Identity.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistByUsername(string username);
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User> GetUserAsync(params Expression<Func<User, bool>>[] filters);
    }
}
