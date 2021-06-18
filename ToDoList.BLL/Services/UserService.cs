using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BLL.ViewModels;
using ToDoList.DAL.Models;
using ToDoList.DAL.Repositories;
using System.Web;
using ToDoList.BLL.Helpers;

namespace ToDoList.BLL.Services
{
    public class UserService
    {
        private IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(UserRegistrationVM model)
        {
            string hashedPassword = HashHelper.ComputeSha256Hash(model.Password);
            User user = new User()
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                Password = hashedPassword
            };
            _userRepository.Add(user);
        }
        
        public bool Login(UserLoginVM model)
        {
            string hashedPassword = HashHelper.ComputeSha256Hash(model.Password);
            if (_userRepository.Get(x => x.Email == model.Email && x.Password == hashedPassword).Any())
                return true;
            else return false;
        }

        public bool IsUserUnique(UserRegistrationVM user)
        {
            if (_userRepository.Get(x => x.Email == user.Email).Any())
                return false;
            else return true;
        }

        public int GetCurrentUserId()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            int userId = _userRepository.Get(x => x.Email == userName).FirstOrDefault().UserId;
            return userId;
        }
    }
}
