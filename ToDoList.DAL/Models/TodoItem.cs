using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DAL.Models
{
    public class TodoItem
    {
        [Key]
        public int TodoItemId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDone { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
