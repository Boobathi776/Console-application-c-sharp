using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.ConsoleApplication.Employee_management_system.Models
{
    public class ContractEmployee : Employee
    {
        public int ContractDurationMonths { get; set; }
        public bool IsRemote { get; set; }
        public ContractEmployee(int empId, string name, string department, decimal salary,
            int contractDurationMonths, bool isRemote) : base(empId, name, department, salary)
        {
            ContractDurationMonths = contractDurationMonths;
            IsRemote = isRemote;
        }
    }
}
