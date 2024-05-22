using Inventec.NetCore.EFCore.ModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.EFCore.Interfaces
{
    public interface IViewRepository<T> : IRepository where T : class, IEntityBase, new()
    {
        IQueryable<T> GetQueryable();

        IQueryable<T> GetTrackingQueryable();

        IQueryable<T> GetQueryableIncludeIsDelete();

        IQueryable<T> GetByRawSql(string sql, params object[] parameters);

        ///<summary>Entities returned from Find are tracked by default, but do not include navigation properties, for that use GetTrackingQueryable()</summary>
        Task<T> FindAsync(string id);

        T Find(string id);

        Task<int> CountAsync();
    }
}
