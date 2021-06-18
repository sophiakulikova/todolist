using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DAL.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        T FindById(int id);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Delete(T entity);
        void Edit(T entity);

    }
}
