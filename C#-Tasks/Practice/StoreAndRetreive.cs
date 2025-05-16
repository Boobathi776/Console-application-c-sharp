using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Practice;
//StoreAndRetreive store = new StoreAndRetreive();
//store.StoreData();
//store.RetrieveData();

namespace Tasks.Practice
{
    internal class StoreAndRetreive
    {
        public void RetrieveData()
        {
            string filePath = @"D:\Azure-Assignment\C#-Tasks\C#-Tasks\Practice\File.txt";
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] contents = line.Split(',');
                        if (contents.Length != 4)
                        {
                            Console.WriteLine("data format is not correct");
                            continue;
                        }
                        int.TryParse(contents[0], out int studentId);
                        string studentName = contents[1];
                        string[] marksString = contents[2].Split(':');
                        if (marksString.Length != 5)
                        {
                            Console.WriteLine("mark format is not correct");
                            continue;
                        }
                        double[] marks = new double[marksString.Length];
                        int i = 0;
                        foreach (var mark in marksString)
                        {
                            double.TryParse(mark, out double parsedMark);
                            marks[i] = parsedMark;
                            i++;
                        }
                        string grade = contents[3];
                        Console.WriteLine($"Student ID: {studentId}");
                        Console.WriteLine($"Student Name: {studentName}");
                        foreach (double mark in marks)
                        {
                            Console.WriteLine($"Marks: {mark}");
                        }
                        Console.WriteLine($"Grade: {grade}");
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
            }
        }

        public void StoreData()
        {
            string filePath = @"D:\Azure-Assignment\C#-Tasks\C#-Tasks\Practice\File.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {

                    int studentId = 2;
                    string studentName = "guhan ";
                    double[] marks = { 90.5, 85.0, 78.5, 45, 90 };
                    string grade = "pass";

                    writer.Write($"{studentId},{studentName},");
                    int i = 0;
                    foreach (var mark in marks)
                    {
                        writer.Write($"{mark}");
                        if (i < marks.Length - 1)
                        {
                            writer.Write(":");
                            i++;
                        }
                    }
                    writer.Write($",{grade}\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    
}
}
