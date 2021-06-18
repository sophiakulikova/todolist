using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL.Models;

namespace ToDoList.BLL.ViewModels
{
    public class TodoItemVM
    {
        public int Id;
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public DateTime? EndDate { get; set; }

        public TodoItemVM()
        {

        }

        public TodoItemVM(TodoItem todoItem)
        {
            Id = todoItem.TodoItemId;
            Name = todoItem.Name;
            IsDone = todoItem.IsDone;
            EndDate = todoItem.EndDate;
        }
    }
}
