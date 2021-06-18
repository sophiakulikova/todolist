using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDoList.DAL;
using ToDoList.DAL.Repositories;

namespace ToDoList.Web.Utilities
{
    public class RequestLivesDbContextProvider : IDbContextProvider
    {
        public DbContext Context
        {
            get
            {
                if (HttpContext.Current.Items["DbContext"] == null)
                {
                    HttpContext.Current.Items["DbContext"] = new ToDoListContext();
                }

                return (DbContext)HttpContext.Current.Items["DbContext"];
            }
        }
    }
}