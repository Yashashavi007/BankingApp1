using System;
using System.Collections.Generic;
using System.Text;

namespace Technovert.BankApp.Models
{
    public class Employee
    {
        public int EID { get; set; }
        public string EName { get; set; }
        public string ERole { get; set; }

        public Employee(string Name, string Role)
        {
            EName = Name;
            ERole = Role;
        }

    }
}
