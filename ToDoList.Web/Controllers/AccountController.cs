using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDoList.BLL.Services;
using ToDoList.BLL.ViewModels;

namespace ToDoList.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            if (!string.IsNullOrEmpty(HttpContext.User.Identity.Name))
                return RedirectToAction("Index","ToDoList");
            return View("CreateAccount");
        }

        [HttpPost]
        public ActionResult CreateAccount(UserRegistrationVM model)
        {
            if (!ModelState.IsValid)
                return View("CreateAccount", model);

            if (!model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "Password do not match");
                return View("CreateAccount", model);
            }

            if (!_userService.IsUserUnique(model))
            {
                ModelState.AddModelError("", "User with this email already exists");
                return View("CreateAccount", model);
            }

            _userService.Register(model);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            string userName = User.Identity.Name;

            if (!string.IsNullOrEmpty(userName))
                return RedirectToAction("Index", "ToDoList");

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool isValid = _userService.Login(model);

            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                return RedirectToAction("Index", "ToDoList");
            }

        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}