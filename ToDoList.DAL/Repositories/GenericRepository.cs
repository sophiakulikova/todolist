using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {

        private readonly IDbContextProvider _contextProvider;

        private DbContext Context
        {
            get { return _contextProvider.Context; }
        }

        private DbSet<T> DbSet
        {
            get { return Context.Set<T>(); }
        }

        public GenericRepository(IDbContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public IEnumerable<T> Get()
        {
            return DbSet;
        }

        public T FindById(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return DbSet.Where(predicate);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public void Edit(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }


        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
