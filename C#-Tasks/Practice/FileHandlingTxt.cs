using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tasks.ConsoleApplication;

namespace Tasks.Practice;

public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeName { get; set; }
}

internal class FileHandlingTxt
{
    //public void CreateFile()
    //{
    //    if (Directory.Exists(@"D:/Practice Folder/"))
    //    {
    //        File.WriteAllText(@"D:/Practice Folder/practice.txt", "Welcome to the world of C#");
    //    }
    //    else
    //    {
    //        Directory.CreateDirectory(@"D:/Practice Folder/");
    //        File.WriteAllText(@"D:/Practice Folder/practice.txt", "Welcome to the world of C#");

    //    }

    //    //Read a entire content from the file 
    //    string path = @"D:/Practice Folder/practice.txt";
    //    string content = File.ReadAllText(path);
    //    Console.WriteLine(content);

    //    //withour replacing append a content to the file 
    //    File.AppendAllText(path, "\nhey hi there i think its working");

    //    //write mulitple line in a sinle time using string array 
    //    string[] lines = { "welcome", "\nnothing to say", "\n i am gonna handle this file", "\nWelcome again and thank you" };
    //    File.AppendAllLines(path, lines);

    //    //Read multiple lines in  a single time and store it in a string array 
    //    string[] readLines = File.ReadAllLines(path);
    //    foreach(string line in readLines)
    //    {
    //        Console.WriteLine(line);

    //    }

    //    //using StreamReader and StreamWriter
    //    using (StreamReader r = new StreamReader(path))
    //    {
    //        string line;
    //        while((line= r.ReadLine())!=null)
    //        {
    //            Console.WriteLine(line);
    //        }
    //    }

    //    using (StreamWriter wr = new StreamWriter(path))
    //    {
    //        wr.WriteLine("i am in market");
    //        wr.WriteLine("i am writing this content using stream writer");
    //    }

    //    using (StreamWriter wr = new StreamWriter(path, append: true) )
    //    {
    //        wr.WriteLine("i am appending a value to the file ");
    //        wr.WriteLine("i am writing this content using stream writer with append mode ");
    //    }

    //    //rename or move a file to another location 
    //    File.Move(path, "D:/Practice Folder/boobathi.txt");
    //    //File.Delete("D:/Practice Folder/boobathi.txt");
    //    //File.CreateText(path);
    //    File.Delete(path);

    public void Enumerator()
    {
        string filepath = ConfigurationManager.AppSettings["Filepath"];
        //string[] files = Directory.GetFiles(filepath, "*.txt", SearchOption.AllDirectories);
        //foreach(var file in files)
        //{
        //    Console.WriteLine(file);
        //}
        //using (StreamWriter writer = new StreamWriter(Path.Combine(filepath, "BoobathiA.txt"), true))
        //{
        //    Employee obj = new Employee() { EmployeeID = 12, EmployeeName = "Boobathi" };
        //    writer.WriteLine(@$"{obj.EmployeeID},{obj.EmployeeName} ");
        //}

        //using (StreamReader reader = new StreamReader(Path.Combine(filepath, "BoobathiA.txt")))
        //{
        //    string line;

        //    while ((line = reader.ReadLine()) != null)
        //    {
        //        string[] fullline = line.Split(',');

        //        int.TryParse(fullline[0], out int ID);
        //            Employee durai = new Employee { EmployeeID = ID , EmployeeName = fullline[1] };

        //        Console.WriteLine(durai.EmployeeName + durai.EmployeeID);
        //    }

        //}
    }

    public void WritingFile()
    {
        //string filePath = ConfigurationManager.AppSettings["Directory path"];
        //Console.WriteLine(filePath);

        //string filename = Console.ReadLine();
        //string fileNameWithPath = Path.Combine(filePath, filename);
        //if(File.Exists(fileNameWithPath))
        //{
        //    File.WriteAllText(fileNameWithPath, "hey naan solren la try panni paaru ");
        //}
        //else
        //{
        //    Console.WriteLine("The file doesn't exist");
        //    File.Create(fileNameWithPath);
        //}

        //Directory.CreateDirectory(Path.GetDirectoryName(@"D:\Workspace\File handling practice\durai\durai.txt"));
        //Console.WriteLine("ok da");
        //using (StreamWriter wroter = new StreamWriter(@"D:\Workspace\File handling practice\durai.txt"))
        //{
        //    wroter.WriteLine("naan than da leo");
        //}
        //int a = 5, b = 6;
        //switch ((a, b))
        //{
        //    case (5, 6) when a < b:
        //        Console.WriteLine("1");
        //        break;
        //}
        //int[] arr = {1,2, 3, 4, 5 };
        //int val=Array.Find(arr,a => a > 3);
        //Console.Write(val);

        List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
        int num = list.Find(l => l >= 3);
        Console.WriteLine(num);
        Console.WriteLine(list.Count);
        Console.WriteLine(list.Count(l => l < 5));

    }

    //for store object data
    public void Handling()
    {
        //FileStream fs = new FileStream(@"D:\Workspace\File handling practice\newFile.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        try
        {
            using (StreamWriter writer = new StreamWriter(@"D:\New Folder\File.txt"))
            {
                int studentId = 1;
                string studentName = "Boobathi ";
                double[] marks = { 90.5, 85.0, 78.5, 45, 90 };
                string grade = "pass";
                writer.WriteLine($"{studentId},{studentName},{marks[0]}:{marks[1]}:{marks[2]}:{marks[3]}:{marks[4]},{grade}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LogFile(e);
        }
        finally
        {
            Console.WriteLine("File created successfully");
        }
    }


    public void StoreLogFils()
    {
        try
        {
            int a = 10;
            int b = 0;
            int c = a / b;
            Console.WriteLine(c);
        }
        catch (ArithmeticException e)
        {
            Console.WriteLine(e.Message);
            LogFile(e);
        }
    }

    public void LogFile(Exception e)
    {
        using (StreamWriter writer = new StreamWriter($@"D:\Workspace\File handling practice\Log{DateTime.Now.ToString("dd-MM-yyyy hh-mm")}.txt", true))
        {
            writer.WriteLine("=======================================================");
            writer.WriteLine("Source: " + e.Source);
            writer.WriteLine("Message : " + e.Message);
            writer.WriteLine("StackTrace: \n" + e.StackTrace + "\n");
            writer.WriteLine("Date: " + DateTime.Now.ToString("dd-mm-yyyy hh-MM-ss"));
            writer.WriteLine("=======================================================");
        }
    }
}

    
