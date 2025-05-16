using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.ConsoleApplication.Employee_management_system.Models
{
    public class Employee
    {
        public int EmployeeId { get; init; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public Employee(int empId, string name, string department, decimal salary)
        {
            EmployeeId = empId;
            Name = name;
            Department = department;
            Salary = salary;
        }

    }
}
