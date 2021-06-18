using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.BLL.Services;
using ToDoList.BLL.ViewModels;

namespace ToDoList.Web.Controllers
{
    public class ToDoListController : Controller
    {
        private UserService _userService;
        private TodoItemService _todoItemService;
        private int _userId;

        public ToDoListController(TodoItemService todoItemService, UserService userService)
        {
            _todoItemService = todoItemService;
            _userService = userService;
        }
        
        [Authorize]
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                return RedirectToAction("Login","Account");
            }
            return View();
        }

        [Authorize]
        public ActionResult Statistics()
        {
            if (string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                return RedirectToAction("Login", "Account");
            }
            _userId = _userService.GetCurrentUserId();
            var todoItemsByDate = _todoItemService.GetStatictics(_userId);
            return View(todoItemsByDate);
        }

        [Authorize]
        public ActionResult BuildToDoTable()
        {
            _userId = _userService.GetCurrentUserId();
            List<TodoItemVM> activeItmes = _todoItemService.GetActiveTodoItems(_userId);
            List<TodoItemVM> doneItems = _todoItemService.GetDoneItems(_userId);
            foreach (var item in doneItems)
            {
                activeItmes.Add(item);
            }
            return PartialView("_ToDoTable", activeItmes);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewToDoItem(string name)
        {
            _userId = _userService.GetCurrentUserId();
            if (ModelState.IsValid)
            {
                TodoItemVM todoItem = new TodoItemVM
                {
                    Name = name
                };
                _todoItemService.AddNewTodo(todoItem, _userId);
            }
            return RedirectToAction("BuildToDoTable");
        }

        [Authorize]
        public ActionResult ChangeIsDoneValue(int id, bool value)
        {
            _todoItemService.EditItemIsDone(id, value);
            return RedirectToAction("BuildToDoTable");
        }

        [Authorize]
        public ActionResult ChangeName(int id, string value)
        {
            _todoItemService.EditItemName(id, value);
            return RedirectToAction("BuildToDoTable");
        }

        [Authorize]
        public ActionResult DeleteToDoItem(int id)
        {
            _todoItemService.DeleteItem(id);
            return RedirectToAction("BuildToDoTable");
        }
    }
}