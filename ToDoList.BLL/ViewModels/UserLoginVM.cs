using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BLL.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
