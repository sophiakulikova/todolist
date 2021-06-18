using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DAL.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(200)]
        public string Password { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; }
    }
}
