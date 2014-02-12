using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Linq.Expressions;

namespace Kask.DAL2.Core
{
    public abstract class BaseRepository<T> where T : class, IEntity
    {
        protected Table<T> Entity;

        public BaseRepository(DataContext _context)
        {
            Entity = _context.GetTable<T>();
        }

        public IQueryable<T> TableContext
        {
            get 
            {
                return Entity.AsQueryable<T>();
            }
        }

        public bool Add(T data)
        {
            if (data == null)
                return false;

            Entity.InsertOnSubmit(data);
            return true;
        }

        public bool Remove(T data)
        {
            Entity.DeleteOnSubmit(data);
            return true;
        }

        public T Get(int id)
        {
            return null;
        }

        public IQueryable<T> SearchFor (Expression<Func<T, bool>> exp)
        {
            return Entity.Where(exp);
        }
    }

    public interface IEntity { }
}
