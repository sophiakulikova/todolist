using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BLL.ViewModels;
using ToDoList.DAL.Models;
using ToDoList.DAL.Repositories;

namespace ToDoList.BLL.Services
{
    public class TodoItemService
    {
        private GenericRepository<TodoItem> _todoItemRepository;
        private GenericRepository<User> _userRepository;

        public TodoItemService(GenericRepository<TodoItem> todoItemRepository, GenericRepository<User> userRepository)
        {
            _todoItemRepository = todoItemRepository;
            _userRepository = userRepository;
        }
        public List<TodoItemVM> GetActiveTodoItems(int userId)
        {
            List<TodoItem> todoItems = _todoItemRepository.Get(x => (x.UserId == userId) && (x.IsDone == false)).OrderByDescending(x => x.TodoItemId).ToList();

            List<TodoItemVM> todoItemVMs = new List<TodoItemVM>();
            foreach (var item in todoItems)
            {
                var todoItemVM = new TodoItemVM(item);
                todoItemVMs.Add(todoItemVM);
            }
            return todoItemVMs;
        }

        public List<TodoItemVM> GetDoneItems(int userId)
        {
            DateTime today = DateTime.Today;
            List<TodoItem> todoItems = _todoItemRepository.Get(x => (x.UserId == userId) && (x.EndDate != null) && (x.EndDate.Value.Date == today)).OrderBy(x => x.EndDate).ToList();

            List<TodoItemVM> todoItemVMs = new List<TodoItemVM>();
            foreach (var item in todoItems)
            {
                var todoItemVM = new TodoItemVM(item);
                todoItemVMs.Add(todoItemVM);
            }
            return todoItemVMs;
        }

        public void AddNewTodo(TodoItemVM todoItemVM, int userId)
        {
            var todoItem = new TodoItem()
            {
                UserId = userId,
                Name = todoItemVM.Name,
                IsDone = false,
                EndDate = null
            };
            _todoItemRepository.Add(todoItem);
        }

        public void EditItemIsDone(int id, bool value)
        {
            TodoItem todoItem = _todoItemRepository.FindById(id);
            todoItem.IsDone = value;
            if (value == true)
                todoItem.EndDate = DateTime.Now;
            else todoItem.EndDate = null;
            _todoItemRepository.Edit(todoItem);
        }

        public void EditItemName(int id, string name)
        {
            TodoItem todoItem = _todoItemRepository.FindById(id);
            todoItem.Name = name;
            _todoItemRepository.Edit(todoItem);
        }

        public void DeleteItem(int id)
        {
            var itemToDelete = _todoItemRepository.FindById(id);
            _todoItemRepository.Delete(itemToDelete);
        }

        public Dictionary<DateTime, List<TodoItemVM>> GetStatictics(int userId)
        {
            DateTime today = DateTime.Today;
            List<TodoItem> todoItems = _todoItemRepository.Get(x => (x.UserId == userId) && (x.IsDone == true) && (x.EndDate.Value.Date < today)).OrderByDescending(x => x.EndDate).ToList();
            List<TodoItemVM> todoItemVMs = new List<TodoItemVM>();
            foreach (var item in todoItems)
            {
                var todoItemVM = new TodoItemVM(item);
                todoItemVMs.Add(todoItemVM);
            }

            Dictionary<DateTime, List<TodoItemVM>> toDoItemsByDate = new Dictionary<DateTime, List<TodoItemVM>>();
            DateTime currentDate = new DateTime();

            foreach (var item in todoItemVMs)
            {
                if (currentDate.Date != item.EndDate.Value.Date)
                {
                    currentDate = item.EndDate.Value.Date;
                    toDoItemsByDate.Add(currentDate, new List<TodoItemVM>());
                }

                toDoItemsByDate[currentDate].Add(item);
            }
            return toDoItemsByDate;
        }
    }
}
