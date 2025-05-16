using System.Diagnostics;
using Tasks.ConsoleApplication.SchoolManagementSystem.Interfaces;

namespace Tasks.SchoolManagementSystem.Models
{
    internal class Student : IStudent
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public double[] Marks { get; set; }

        public virtual void DisplayStudentDetails(List<string> subjects)
        {
            Console.Write($"\n{StudentID}{StudentName}\t");
            int i = 1;
            foreach (var mark in Marks)
            {
                Console.Write($"{mark}\t");
            }
        }
    }
}
