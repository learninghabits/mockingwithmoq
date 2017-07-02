using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace mocking_with_moq.Repositories
{
    public interface IRepository<T>
     where T : class
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        void Delete(int id);
        void Dispose();
        T Find(int id);
        void Insert(T t);
        void Save();
        void Update(T t);
        IList<T> Where(Expression<Func<T, bool>> filter);
    }
}
