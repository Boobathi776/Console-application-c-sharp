using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.ConsoleApplication.Employee_management_system.Interfaces
{
    interface IEmployeeManagementSystem
    {
        int GetEmployeeId();
        string GetEmployeeName();
        string GetEmployeeDepartment();
        decimal GetEmployeeSalary();
        DateOnly GetJoiningDate();
        bool HasInsuranceCoverage();
        int LeaveEncashmentBalance();
        int GetContractDuration();
        bool GetIsRemote();

        void Management()
        {
            Console.WriteLine("Here all the employee details are getting managed");
        }
    }
}
