using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.ConsoleApplication.Employee_management_system.Models
{
    public class PermanentEmployee : Employee
    {
        public DateOnly JoiningDate { get; set; }
        public bool HasInsuranceCoverage { get; set; }
        public int LeaveEncashmentBalance { get; set; }
        public PermanentEmployee(int empId, string name, string department,
            decimal salary, DateOnly joiningDate, bool hasInsuranceCoverage,
            int leaveEncashmentBalance) : base(empId, name, department, salary)
        {
            JoiningDate = joiningDate;
            HasInsuranceCoverage = hasInsuranceCoverage;
            LeaveEncashmentBalance = leaveEncashmentBalance;
        }
    }
}
