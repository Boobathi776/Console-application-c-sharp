using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.ConsoleApplication;

namespace Tasks.SchoolManagementSystem.Models
{
    internal class RegularStudent : Student
    {
        public string Grade { get; set; }
        public RegularStudent(int studentID , string studentName , double[] studentMarks,string grade)
        {
            StudentID = studentID;
            StudentName = studentName;
            Marks = studentMarks;
            Grade = grade;
        }

        public override void DisplayStudentDetails(List<string> subjects)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\n\t{StudentID,-12}{StudentName,-18}");
            int countOFSubject = subjects.Count();
            foreach (var mark in Marks)
            {
                Console.Write($"{mark,-10}");
                countOFSubject--;
            }
            for(int i=0;i<countOFSubject;i++)
            {
                Console.Write($"{0,-10}");
            }
            Console.Write($"{Grade,-10}\n");
            Console.ResetColor();
        }
    }
}
