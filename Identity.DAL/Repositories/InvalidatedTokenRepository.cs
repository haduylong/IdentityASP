using Identity.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Repositories
{
    public interface IInvalidatedTokenRepository
    {

    }

    public class InvalidatedTokenRepository : GenericRepository<InvalidatedToken, string>, IInvalidatedTokenRepository
    {
        public InvalidatedTokenRepository(AppDbContext context) : base(context)
        {
        }
    }
}
