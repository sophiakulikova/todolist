using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Common.Helpers
{
    public class ConfigurationHelper
    {
        public static string DbConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ToString();
    }
}
