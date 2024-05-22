using Inventec.NetCore.EFCore.ModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.EFCore.Interfaces
{
    public interface IRepository
    {
    }
    public interface IRepository<T> : IRepository where T : class, IEntityBase, new()
    {
        IQueryable<T> GetQueryable();

        IQueryable<T> GetTrackingQueryable();

        IQueryable<T> GetQueryableIncludeIsDelete();

        ///<summary>Entities returned from Find are tracked by default, but do not include navigation properties, for that use GetTrackingQueryable()</summary>
        Task<T> FindAsync(string id);

        T Find(string id);

        void Add(T entity);

        Task AddAsync(T entity, CancellationToken token = default);

        void AddRange(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        IQueryable<T> GetByRawSql(string sql, params object[] parameters);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void RemoveRange(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
