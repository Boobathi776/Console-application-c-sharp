//namespace Tasks.ConsoleApplication;
namespace Tasks.ConsoleApplication;

using System;
using Tasks.Practice;
using Tasks.SchoolManagementSystem.Services;
using Tasks.Helpers;
using  Tasks.ConsoleApplication.Employee_management_system.Interfaces;
using Tasks.ConsoleApplication.Library_Book_management_system.Services;
using Tasks.ConsoleApplication.Inventory_Management_System.Validators;
using Tasks.ConsoleApplication.Inventory_Management_System.Services;

//using Tasks.Practice;

class Program
{
    static void Main(string[] args)
    {

        //HelloWorld hello = new HelloWorld();
        //hello.Hello();

        //Calculator calc = new Calculator();
        //calc.callCalculator();

        // 2 . calculator using type conversion
        //CalculatorTypeConversion calculatorTypeConversion = new CalculatorTypeConversion();
        //calculatorTypeConversion.Calculating();

        // 3 . find the second largest number
        //SecondNumber secondNumber = new SecondNumber();
        //int[] arr = secondNumber.getValue();
        //secondNumber.findSecondNumber(arr);

        // 4. ATM menu
        //ATMmenu atm = new ATMmenu();
        //atm.banking();

        // 5. Safe Number Collector
        //NumberCollector collector = new NumberCollector();
        //collector.collecting();

        // 6. GislenSoftware with a Twist
        //GislenTwist twist = new GislenTwist();
        //twist.print();

        //7.Reverse & Averaged
        //ReverseArray reverseArray = new ReverseArray();
        //reverseArray.getValue();
        //reverseArray.reverse();
        //reverseArray.printValue();
        //reverseArray.average();

        // 8 . Age Category Checker
        //AgeCategory age = new AgeCategory();
        //age.checkAge();

        // 9 . Student Marks Report
        //StudentReport student = new StudentReport();
        //student.assignGrade();


        //Monday 28-04-2025
        // Super market Console Application
        //Console.WriteLine("*************************************************");
        //Console.WriteLine("******       Welcome to Super Market       ******");
        //Console.WriteLine("*************************************************");


        //The code that i wrote in test
        //SuperMarket superMarket = new SuperMarket();
        //superMarket.Purchasing();

        //Based on the feedback updated code
        //SuperMarketDynamic superMarket = new SuperMarketDynamic();
        //superMarket.Purchasing();

        //using oops concept 
        //SuperMarketBillingSystem superMarket = new SuperMarketBillingSystem();
        //superMarket.Purchasing();

        //using collections
        //ExpenseTracker expenseTracker = new ExpenseTracker();
        //expenseTracker.Tracker();

        //Student Marks Report using class , object and generic collections 
        //StudentMarksReport studentMarksReport = new StudentMarksReport(100);
        //studentMarksReport.StudentMarkSystem();


        //IEmployeeManagementSystem employeeManagementSystem = new GislenEmployeeManagementSystem();
        //employeeManagementSystem.Management();
        //employeeManagementSystem.Display();


        //FileManagerConsoleApp filemanager = new FileManagerConsoleApp();
        //filemanager.CRUD();


        //09-05-2025 interim assessment question
        //Console.WriteLine("\n*************************************************************************************************\n");
        //Console.WriteLine("***************************         Student Management System        ********************************");
        //Console.WriteLine("\n*************************************************************************************************\n");
        //Console.Write("Enter mark per subject : ");
        //int inputMark = Helper.GetInteger(Console.ReadLine());
        //StudentServices employeeServices = new StudentServices(inputMark);
        //employeeServices.Management();

        //BookService bookService = new BookService();
        //bookService.Librarian();

      InventoryServices inventoryServices = new InventoryServices();
      inventoryServices.InventoryService();


    }
}