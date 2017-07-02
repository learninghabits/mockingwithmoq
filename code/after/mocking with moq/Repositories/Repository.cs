using mocking_with_moq.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace mocking_with_moq.Repositories
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class 
    {
        private TopicsDataContext _dataContext = new TopicsDataContext();
       
        public IQueryable<T> All
        {
            get
            {
                return _dataContext.Set<T>();
            }
        }

        public IList<T> Where(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dataContext.Set<T>().Where(filter);           
            return query.ToList();
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dataContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
        
        public T Find(int id)
        {
            return _dataContext.Set<T>().Find(id);
        }

        public void Insert(T t)
        {           
                _dataContext.Set<T>().Add(t);
        }

        public void Update(T t)
        {
            _dataContext.Entry(t).State = EntityState.Modified;
        }
           
        public void Delete(int id)
        {
            var t = _dataContext.Set<T>().Find(id);
            _dataContext.Entry(t).State = EntityState.Deleted;           
        }

        public void Save()
        {
            try
            {
                _dataContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (DbEntityValidationException)
            {

                throw;
            }
            catch (EntityException)
            {                
                throw;
            }
            
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
