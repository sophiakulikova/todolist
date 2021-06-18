using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DAL.Repositories
{
    public interface IDbContextProvider
    {
        DbContext Context { get; }
    }
}
