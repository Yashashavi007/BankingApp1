using System;
using System.Collections.Generic;
using System.Text;

namespace Technovert.BankApp.Models
{
    public class Bank
    {
        public string BankId { get; set; }
        public string IFSCCode { get; set; }
        public string Name { get; set; }
        public List<Customer> CustomerDetails { get; set; }
        public List<Employee> EmployeeDetails { get; set; }

        public Bank(string name, string IFSCCode)
        {
            this.Name = name;
            this.IFSCCode = IFSCCode;
            CustomerDetails = new List<Customer>();
            EmployeeDetails = new List<Employee>();
        }
    }
}
