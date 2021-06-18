using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Helpers;
using ToDoList.DAL.Models;

namespace ToDoList.DAL
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext()
            : base(ConfigurationHelper.DbConnectionString)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
