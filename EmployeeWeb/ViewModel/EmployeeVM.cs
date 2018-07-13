using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWeb.ViewModel
{
    public class EmployeeVM
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int? Salary { get; set; }
    }
}