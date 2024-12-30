using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Repositories
{
    public interface IGenericRepository<T, ID> where T : class
    {
        /// <summary>
        /// Lấy tất cả các instance của entity kiểu <typeparamref name="T"/>
        /// </summary>
        /// <returns>
        /// Một <see cref="Task"/> chứa danh sách các instance thuộc kiểu <typeparamref name="T"/> 
        /// </returns>
        /// <typeparam name="T">Kiểu của entity.</typeparam>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Lấy tất cả các instance của entity kiểu <typeparamref name="T"/> thoả mãn các điều kiện được cung cấp.
        /// </summary>
        /// <param name="filters">
        /// Một hoặc nhiều biểu thức điều kiện để lọc dữ liệu, ví dụ: <c>t => t.Id == 1</c>.
        /// Mỗi điều kiện trong <paramref name="filters"/> sẽ được áp dụng để lọc các instance trong entity.
        /// </param>
        /// <returns>
        /// Một <see cref="Task"/> chứa danh sách các instance thuộc kiểu <typeparamref name="T"/> 
        /// thoả mãn tất cả các điều kiện trong <paramref name="filters"/>.
        /// </returns>
        /// <remarks>
        /// Nếu không có điều kiện nào được cung cấp, phương thức sẽ trả về tất cả các instance.
        /// </remarks>
        /// <typeparam name="T">Kiểu của entity.</typeparam>
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, bool>>[] filters);
        Task<T> GetAsync(ID id);
        Task<T> GetAsync(params Expression<Func<T, bool>>[] filters);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(ID id);
    }
}
