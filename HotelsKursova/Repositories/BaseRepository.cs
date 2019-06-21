using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace Repositories
{

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly HotelsEntities1 Context;

        public BaseRepository()
            : this(new HotelsEntities1())
        {

        }
        public BaseRepository(HotelsEntities1 context)
        {
            Context =  context;
        }
        protected DbSet<T> DbSet
        {
            get
            {
                return Context.Set<T>();
            }
        }
        public void Create(T item)
        {
            Context.Set<T>().Add(item);
            Context.SaveChanges();
        }


        public void Delete(T obj)
        {
            if (obj != null)
            {
                Context.Set<T>().Remove(obj);
            }
        }
        public void DeleteByID(int id)
        {
            //bool isDeleted = false;
            T dbItem = Context.Set<T>().Find(id);
            if (dbItem != null)
            {
                Context.Set<T>().Remove(dbItem);
                //int recordsChanged = Context.SaveChanges();
                //isDeleted = recordsChanged > 0;
            }
            // return isDeleted;
        }

        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public T GetByID(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Update(T item, Func<T, bool> findByIDPredecate)
        {
            var local = Context.Set<T>()
                         .Local
                         .FirstOrDefault(findByIDPredecate);
            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            Context.Entry(item).State = EntityState.Modified;
        }

        public abstract void Save(T item);
    }
}


