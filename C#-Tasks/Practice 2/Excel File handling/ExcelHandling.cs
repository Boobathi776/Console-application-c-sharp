using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using Tasks.Practice_2;

//ExcelHandling excelHandling = new ExcelHandling();
//excelHandling.Excel();
//excelHandling.ReadFromExcel();


namespace Tasks.Practice_2
{
    public class Employee
    {
        public Employee() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
        public Employee(int id, string name, double salary, string department)
        {
            Id = id;
            Name = name;
            Salary = salary;
            Department = department;
        }
    }

    internal class ExcelHandling
    {
        string filePath = @"D:\Azure-Assignment\C#-Tasks\C#-Tasks\Practice 2\Excel File handling\Files\employee_Details.xlsx";
        List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Salary = 50000, Department = "HR" },
            new Employee { Id = 2, Name = "Jane Smith", Salary = 60000, Department = "IT" },
            new Employee { Id = 3, Name = "Sam Brown", Salary = 55000, Department = "Finance" }
        };
        public void Excel()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Employee details");
                workSheet.Cell("A1").Value = "Employee Id";
                workSheet.Cell("B1").Value = "Employee Name";
                workSheet.Cell("C1").Value = "Employee Salary";
                workSheet.Cell("D1").Value = "Department";
                foreach (var employee in employees)
                {
                    workSheet.Cell("A" + (employees.IndexOf(employee) + 2)).Value = employee.Id;
                    workSheet.Cell("B" + (employees.IndexOf(employee) + 2)).Value = employee.Name;
                    workSheet.Cell("C" + (employees.IndexOf(employee) + 2)).Value = employee.Salary;
                    workSheet.Cell("D" + (employees.IndexOf(employee) + 2)).Value = employee.Department;
                }

                workBook.SaveAs(filePath);
            }
        }

        public void ReadFromExcel()
        {
            List<string> details = new List<string>();
            using (var workBook = new XLWorkbook(filePath))
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
                    if (details.Count() == 4)
                    {
                        int id = int.TryParse(details[0], out int parsedId) ? parsedId : 0;
                        string name = details[1];
                        double salary = double.TryParse(details[2], out double parsedSalary) ? parsedSalary : 0;
                        string department = details[2];
                        Console.WriteLine($"Id: {id}, Name: {name}, Salary: {salary}, Department: {department}");
                        details.Clear();
                        employeeCount++;
                    }
                }
                Console.WriteLine($"No of details read from the excel file is : {employeeCount - 1}");
            }
        }
    }
}
