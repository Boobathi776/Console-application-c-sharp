using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.ConsoleApplication.SchoolManagementSystem.Interfaces
{
    public interface IStudent
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public double[] Marks { get; set; }

        public void DisplayStudentDetails(List<string> subjects);
    }
}
