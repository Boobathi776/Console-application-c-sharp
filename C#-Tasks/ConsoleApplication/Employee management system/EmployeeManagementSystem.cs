
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml.Linq;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlTypes;
using Tasks.ConsoleApplication.Employee_management_system.Models;
using Tasks.ConsoleApplication.Employee_management_system.Interfaces;
namespace Tasks.ConsoleApplication;

internal class GislenEmployeeManagementSystem : IEmployeeManagementSystem
{
    List<PermanentEmployee> permanetEmployees = new List<PermanentEmployee>();

    List<ContractEmployee> contractEmployees = new List<ContractEmployee>();

    public  void Management()
    {
        Console.WriteLine("\n*************************************************************************************************\n");
        Console.WriteLine("                                 EMPLOYEE MANAGEMENT SYSTEM                                          ");
        Console.WriteLine("\n*************************************************************************************************\n");
        ReadFromExcel();
        ShowEmployees();
        UpdateEmployeeDetails();
    }

    public void ReadFromExcel()
    {
        FileInfo fileInfo = new FileInfo(@"D:\Azure-Assignment\C#-Tasks\C#-Tasks\ConsoleApplication\Employee management system\Employee excel files\Employee records.xlsx");
        List<string> details = new List<string>();
        using (var workBook = new XLWorkbook(fileInfo.FullName))
        {
            var workSheet = workBook.Worksheet(1);
            var rows = workSheet.RangeUsed().RowsUsed();
            int employeeCount = 0;
            foreach (var row in rows)
            {
                var cells = row.Cells();
                if (employeeCount == 0) { employeeCount++; continue; }
                foreach (var cell in cells)
                {
                    details.Add(cell.GetValue<string>());
                }
                if (details.Count() == 8 )
                {
                    try
                    {
                        int id = int.TryParse(details[0], out int parsedId) ? parsedId : 0;
                        string name = details[1];
                        string department = details[2];
                        decimal salary = decimal.TryParse(details[3], out decimal parsedSalary) ? parsedSalary : 0;
                        DateOnly joiningDate = DateOnly.Parse(details[4]);
                        bool hasInsuranceCoverage = bool.Parse(details[5]);
                        int leaveEncashmentCount = int.Parse(details[6]);
                        permanetEmployees.Add(new PermanentEmployee(id, name, department, salary, joiningDate, hasInsuranceCoverage, leaveEncashmentCount));
                        //Console.WriteLine($"{id} : {name} : {department} : {salary} : {joiningDate} : {hasInsuranceCoverage} : {leaveEncashmentCount}");
                        details.Clear();
                        employeeCount++;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Sorry wait for a moment! unable to convert the value from the excel files\n"+e.Message);
                    }
                }
            }
            Console.WriteLine($"No of permanent employees : {employeeCount - 1}");
        }


        using (var workBook = new XLWorkbook(fileInfo.FullName))
        {
            var workSheet = workBook.Worksheet(2);
            var rows = workSheet.RangeUsed().RowsUsed();
            int employeeCount = 0;
            foreach (var row in rows)
            {
                var cells = row.Cells();
                if (employeeCount == 0) { employeeCount++; continue; }
                foreach (var cell in cells)
                {
                    details.Add(cell.GetValue<string>());
                }
                if (details.Count() == 7)
                {
                    try
                    {
                        int id = int.TryParse(details[0], out int parsedId) ? parsedId : 0;
                        string name = details[1];
                        string department = details[2];
                        decimal salary = decimal.TryParse(details[3], out decimal parsedSalary) ? parsedSalary : 0;
                        int contractDurationMonths = int.Parse(details[4]);
                        bool isRemote = bool.Parse(details[5]);
                        contractEmployees.Add(new ContractEmployee(id,name,department,salary,contractDurationMonths,isRemote));
                        details.Clear();
                        employeeCount++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Sorry wait for a moment! unable to convert the value from the excel files\n" + e.Message);
                    }
                }
            }
            Console.WriteLine($"No of Contract employees : {employeeCount - 1}");
        }
    }

    public void WriteInExcel()
    {
        FileInfo fileInfo = new FileInfo(@"D:\Azure-Assignment\C#-Tasks\C#-Tasks\ConsoleApplication\Employee management system\Employee excel files\Employee records.xlsx");


        using (var workBook = new XLWorkbook())
        {
            var workSheet = workBook.AddWorksheet("Permanent Employee");
            workSheet.Cell("A1").Value = "Employee id";
            workSheet.Cell("B1").Value = "Name";
            workSheet.Cell("C1").Value = "Department";
            workSheet.Cell("D1").Value = "Salary";
            workSheet.Cell("E1").Value = "Joining date";
            workSheet.Cell("F1").Value = "Has insurance coverage";
            workSheet.Cell("G1").Value = "Leave encashment balance";
            workSheet.Cell("h1").Value = "Type of employee";
            workSheet.Range("A1:H1").Style.Font.Bold = true;
            int row = 2;
            foreach (var employee in permanetEmployees)
            {
                workSheet.Cell($"A{permanetEmployees.IndexOf(employee) + 2}").Value = employee.EmployeeId;
                workSheet.Cell($"B{permanetEmployees.IndexOf(employee) + 2}").Value = employee.Name;
                workSheet.Cell($"C{permanetEmployees.IndexOf(employee) + 2}").Value = employee.Department;
                workSheet.Cell($"D{permanetEmployees.IndexOf(employee) + 2}").Value = employee.Salary;
                workSheet.Cell($"E{permanetEmployees.IndexOf(employee) + 2}").Value = employee.JoiningDate.ToString();
                workSheet.Cell($"F{permanetEmployees.IndexOf(employee) + 2}").Value = employee.HasInsuranceCoverage;
                workSheet.Cell($"G{permanetEmployees.IndexOf(employee) + 2}").Value = employee.LeaveEncashmentBalance;
                workSheet.Cell($"h{permanetEmployees.IndexOf(employee) + 2}").Value ="Permanent";
                row++;
            }

            var workSheet2 = workBook.AddWorksheet("Contract Employee");
            workSheet2.Cell("A1").Value = "Employee id";
            workSheet2.Cell("B1").Value = "Name";
            workSheet2.Cell("C1").Value = "Department";
            workSheet2.Cell("D1").Value = "Salary";
            workSheet2.Cell("E1").Value = "Contract Duration Months";
            workSheet2.Cell("F1").Value = "Is Remote Job";
            workSheet2.Cell("G1").Value = "Type of employee";
            workSheet2.Range("A1:G1").Style.Font.Bold = true;
            row = 2;
            foreach (var employee in contractEmployees)
            {
                workSheet2.Cell($"A{row}").Value = employee.EmployeeId;
                workSheet2.Cell($"B{row}").Value = employee.Name;
                workSheet2.Cell($"C{row}").Value = employee.Department;
                workSheet2.Cell($"D{row}").Value = employee.Salary;
                workSheet2.Cell($"E{row}").Value = employee.ContractDurationMonths;
                workSheet2.Cell($"F{row}").Value = employee.IsRemote;
                workSheet2.Cell($"G{row}").Value = "Contract";
                row++;
            }
            workBook.SaveAs(fileInfo.FullName);
        }
   }
    

    private void ShowEmployees()
    {
        Console.WriteLine("Permanent Employees:");
        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine($"\t{"ID",-5}{"Name",-15}{"Department",-15}{"Salary",-10}{"Joining Date",-15}{"Insurance Coverage",-20}{"Leave Encashment Balance",-15}");
        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
        foreach (var employee in permanetEmployees)
        {
            Console.WriteLine($"\t{employee.EmployeeId,-5}{employee.Name,-15}{employee.Department,-15}{employee.Salary,-10}{employee.JoiningDate,-20}" +
             $"{employee.HasInsuranceCoverage,-20}{employee.LeaveEncashmentBalance,-15}");
        }
        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");

        Console.WriteLine("\nContract Employees:");
        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine($"\t{"ID",-5}{"Name",-15}{"Department",-15}{"Salary",-10}{"Contract Duration Months",-26}{"Is Remote",-10}");
        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
        foreach (var employee in contractEmployees)
        {
            Console.WriteLine($"\t{employee.EmployeeId,-5}{employee.Name,-15}{employee.Department,-15}{employee.Salary,-15}" +
          $"{employee.ContractDurationMonths,-24}{employee.IsRemote,-10}");
        }
        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
    }

    private void UpdateEmployeeDetails()
    {
        Console.WriteLine("\n1. Create a New Permanent Employee\n2. Create a New Contract Employee\n3. Delete an Employee\n4. Exit");
        int option = GetOption();
        do
        {
            if (option == 1)
            {
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                Console.WriteLine("Creating a new Permanent Employee...");
                int employeeId = GetEmployeeId();

                while (contractEmployees.Any(employee => employee.EmployeeId == employeeId) || permanetEmployees.Any(employee => employee.EmployeeId == employeeId))
                {
                    Console.Write("Employee ID already exists. Please enter a unique Employee ID....");
                    employeeId = GetEmployeeId();
                }
                string employeeName = GetEmployeeName();
                string employeeDepartment = GetEmployeeDepartment();
                decimal employeeSalary = GetEmployeeSalary();
                DateOnly joiningDate = GetJoiningDate();
                bool hasInsuranceCoverage = HasInsuranceCoverage();
                int leaveEncashmentBalance = LeaveEncashmentBalance();
                PermanentEmployee newPermanentEmployee = new PermanentEmployee(employeeId, employeeName, employeeDepartment, employeeSalary, joiningDate, hasInsuranceCoverage, leaveEncashmentBalance);

                permanetEmployees.Add(newPermanentEmployee);

                Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                ShowEmployees();
                WriteInExcel();
            }
            else if (option == 2)
            {
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                Console.WriteLine("Creating a new Contract Employee...");
                int employeeId = GetEmployeeId();

                while (contractEmployees.Any(employee => employee.EmployeeId == employeeId) || permanetEmployees.Any(employee => employee.EmployeeId == employeeId))
                {
                    Console.Write("Employee ID already exists. Please enter a unique Employee ID....");
                    employeeId = GetEmployeeId();
                }
                string employeeName = GetEmployeeName();
                string employeeDepartment = GetEmployeeDepartment();
                decimal employeeSalary = GetEmployeeSalary();
                int contractDuration = GetContractDuration();
                bool isRemote = GetIsRemote();
                ContractEmployee contractEmployee = new ContractEmployee(employeeId, employeeName, employeeDepartment, employeeSalary, contractDuration, isRemote);
                contractEmployees.Add(contractEmployee);
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                ShowEmployees();
                WriteInExcel();
            }
            else if (option == 3)
            {
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                Console.WriteLine("Removing employee from the list...");
                Console.WriteLine("Enter Employee ID to remove:");
                int employeeId = GetEmployeeId();
                var employeeToRemove = permanetEmployees.FirstOrDefault(e => e.EmployeeId == employeeId);
                if (employeeToRemove != null)
                {
                    permanetEmployees.Remove(employeeToRemove);
                    Console.WriteLine($"Employee with ID {employeeId} removed from the list.");
                    Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                }
                else
                {
                    ContractEmployee employeeToRemove2 = contractEmployees.FirstOrDefault(employee => employee.EmployeeId == employeeId);
                    if (employeeToRemove2 != null)
                    {
                        contractEmployees.Remove(employeeToRemove2);
                        Console.WriteLine($"Employee with ID {employeeId} removed from the list.");
                        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                    }
                    else
                    {
                        Console.WriteLine($"Employee with ID {employeeId} not found.");
                        Console.WriteLine("Enter a valid Employee ID to remove:");
                        employeeId = GetEmployeeId();
                    }
                }
                ShowEmployees();
                WriteInExcel();
            }
            else if (option == 4)
            {
                Console.WriteLine("Exiting...");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
            Console.WriteLine("\n1. Create a New Permanent Employee\n2. Create a New Permanent Employee\n3. Delete an Employee\n4. Exit");
            option = GetOption();
        } while (option != 4);
    }


    /** Methods that used for get input from the user with input validation */
    public int GetOption()
    {
        Console.Write("Enter your choice (1-4):");
        int option;
        while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 4)
        {
            Console.Write("Invalid input. Please enter a number between 1 and 4 : ");
        }
        return option;
    }
     public int GetEmployeeId()
    {
        Console.Write("Enter Employee ID:");
        int empId;
        while (!int.TryParse(Console.ReadLine(), out empId))
        { 
            Console.Write("Invalid input. Please enter a valid Employee ID.");
        }
        return empId;
    }

    public string GetEmployeeName()
    {
        Console.Write("Enter Employee Name:");
        string name = Console.ReadLine();
        bool isName=string.IsNullOrEmpty(name);
        while (!(!isName && !int.TryParse(name, out int result) && name.All(letter =>  Char.IsLetter(letter) || letter == '-' || letter == ' ' || letter == '_') ))
        {
            Console.Write("Please enter a valid Employee Name : ");
            name = Console.ReadLine()??"Invalid name";
        }
        return name;
    }

    public string GetEmployeeDepartment()
    {
        string department;
        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Available Departments:");
        Console.WriteLine("* Finance\n* Developer\n* Admin\n* Testing");
        List<string> departments = new List<string>() { "finance", "developer", "admin", "testing" };
        Console.Write("Enter Employee Department:");
        department = Console.ReadLine();
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        if (!int.TryParse(department, out int result)  && departments.Contains(department.ToLower()))
        {
            return textInfo.ToTitleCase(department.ToLower());  // return the word with first letter upper case 
        }
        Console.Write("Please enter a valid Employee Department : ");
        return GetEmployeeDepartment();
    }

    public decimal GetEmployeeSalary()
    {
        Console.Write("Enter Employee Salary:");
        decimal salary;
        while (!decimal.TryParse(Console.ReadLine(), out salary))
        {
            Console.Write("Please enter a valid Salary: ");
        }
        return salary;
    }

    public DateOnly GetJoiningDate()
    {
        Console.Write("Enter Joining Date (yyyy-mm-dd):");
        DateOnly joiningDate;
        while (!DateOnly.TryParse(Console.ReadLine(), out joiningDate))
        {
            Console.Write("Please enter a valid date (yyyy-mm-dd): ");
        }
        return joiningDate;
    }

    public bool HasInsuranceCoverage()
    {
        Console.Write("Does the employee have insurance coverage? (y/n):");
        string hasInsuranceCoverage = Console.ReadLine().ToLower().Trim();
        if (hasInsuranceCoverage == "y")
        {
            return true;
        }
        else if (hasInsuranceCoverage == "n")
        {
            return false;
        }
        else
        {
            Console.Write("Please enter a valid response (y/n): ");
            return HasInsuranceCoverage();
        }
    }

    public int LeaveEncashmentBalance()
    {
        Console.Write("Enter Leave Encashment Balance:");
        int leaveEncashmentBalance;
        while (!int.TryParse(Console.ReadLine(), out leaveEncashmentBalance))
        {
            Console.Write("Please enter a valid Leave Encashment Balance: ");
        }
        return leaveEncashmentBalance;
    }

    public int GetContractDuration()
    {
        Console.Write("Enter Contract Duration (in months):");
        int contractDuration;
        while (!int.TryParse(Console.ReadLine(), out contractDuration))
        {
            Console.Write("Please enter a valid contract duration: ");
        }
        return contractDuration;

    }

    public bool GetIsRemote()
    {
        Console.Write("Is the employee working remotely? (y/n):");
        string isRemote = Console.ReadLine().ToLower().Trim();
        if (isRemote == "y")
        {
            return true;
        }
        else if (isRemote == "n")
        {
            return false;
        }
        else
        {
            Console.Write("Please enter a valid response (y/n): ");
            return GetIsRemote();
        }
    }

}