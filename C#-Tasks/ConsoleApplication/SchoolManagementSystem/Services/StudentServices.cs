using System.Configuration;
using System.Linq.Expressions;
using Microsoft.VisualBasic;
using Tasks.Helpers;
using Tasks.SchoolManagementSystem.Models;
using Tasks.SchoolManagementSystem.Services;

/*  The main method code snippet  top level statements*/
//Console.WriteLine("\n*************************************************************************************************\n");
//Console.WriteLine("***************************         Student Management System        ****************************");
//Console.WriteLine("\n*************************************************************************************************\n");
//Console.Write("Enter mark per subject : ");
//int inputMark = Helper.GetInteger(Console.ReadLine());
//StudentServices studentServices = new StudentServices(inputMark);
//studentServices.Management();

namespace Tasks.SchoolManagementSystem.Services
{
    internal class StudentServices
    {
        readonly int markPerSubject;
        public StudentServices(int markPerSubject)
        {
            this.markPerSubject = markPerSubject;
        }

        List<string> subjects = new List<string>() { "Tamil", "English", "Maths", "Science", "Social"};
        List<Student> students = new List<Student>();
        string directoryPath = ConfigurationManager.AppSettings["StudentDirectory"];  //directory for student details 

        public void Management()
        {
            ReadFromFile();
            int option;
            do
            {
                Console.WriteLine("1. Add Student\n2. Update Student\n3. Remove Student\n4. Display Student Details\n5. Dispaly particular student\n6. Grades of Students \n7. Exit");
                Console.Write("Enter the option that you want to do(1-7) : ");
                option = GetOption();
                
                switch (option)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        UpdateStudent();
                        break;
                    case 3:
                        RemoveStudent();
                        break;
                    case 4:
                        DisplayStudentDetails();
                        break;
                    case 5:
                        DisplayOneStudentDetails();
                        break;
                    case 6:
                        Console.WriteLine("Based on group details ");
                        GroupStudentsBasedOnGrade();
                        break;
                    case 7:
                        Console.WriteLine("Exiting....");
                        break;
                    default:
                        Console.WriteLine("Enter a valid option : ");
                        break;
                }
                Console.WriteLine("\n-------------------------------------------------------------------------------------------\n");
            } while (option != 7);
        }

        public void ReadFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine(directoryPath, "Regular Student Details.txt")))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        try
                        {
                            string[] studentData = line.Split(",");
                            if (studentData.Length != 4)
                            {
                                Console.WriteLine("(Regular Student details)The data format is not correct while Split with ' , '");
                                continue;
                            }
                            int.TryParse(studentData[0], out int studentId);
                            string studentName = studentData[1];
                            string[] marks = studentData[2].Split(':');
                            if (marks.Length != subjects.Count())
                            {
                                Console.WriteLine("(Regular Student details)The data format is not correct while Split with ' : '");
                                continue;
                            }
                            double[] marksOfStudent = new double[marks.Length];
                            int i = 0;
                                foreach (var mark in marks)
                                {
                                    try
                                    {
                                        double.TryParse(mark, out double score);
                                        marksOfStudent[i] = score;
                                        i++;
                                    }
                                    catch (Exception e)
                                    {
                                        LogError(e);
                                         Console.WriteLine("unable to fetch the data from the file!");
                                    }
                                }
                            string grade = studentData[studentData.Length-1];
                            students.Add(new RegularStudent(studentId,studentName, marksOfStudent, studentData[3]));
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Array size is not fit to store the details");
                            LogError(e);
                        }

                    }
                }

                using (StreamReader reader = new StreamReader(Path.Combine(directoryPath, "Exchange Student Details.txt")))
                {
                    string line;
                    
                    while ((line = reader.ReadLine()) != null)
                    {
                        try
                        {
                            string[] studentData = line.Split(",");
                            if (studentData.Length != 4)
                            {

                                Console.WriteLine("(Exchange Student details)The data format is not correct at Split with ' , '");
                                continue;
                            }
                            int.TryParse(studentData[0], out int studentId);
                            string studentName = studentData[1];
                            var marks = studentData[2].Split(":");
                            if(marks.Length != subjects.Count())
                            {
                                Console.WriteLine("(Exchange Student details)The data format is not correct while split ' : ' ");
                                continue;
                            }
                            double[] marksOfStudent = new double[marks.Length];
                            int i = 0;
                                foreach (var mark in marks)
                                {
                                    try
                                    {
                                        double.TryParse(mark, out double score);
                                        marksOfStudent[i] = score;
                                        i++;
                                    }
                                    catch (Exception e)
                                    {
                                        LogError(e);
                                            Console.WriteLine("unable to fetch the details from the file!");
                                    }
                                }
                            students.Add(new ExchangeStudent(studentId, studentData[1], marksOfStudent, studentData[3]));
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Array size is not fit for this student details..");
                            LogError(e);
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                LogError(e);
                Console.WriteLine(e.Message);
                Console.WriteLine("Sorry your file is not here...");
            }
            catch(Exception e)
            {
                LogError(e);
                Console.WriteLine("unable to handle the data that get from the file.");
            }
        }

       


        //remove a student details in a file
        public void UpdateRemovedStudentInFile(Student student)
        {
            try
            {
                Student selectedStudent = null;
                string[] allLines = null;
                string fullPath = "";
                if (student is RegularStudent regularStudent)
                {
                    selectedStudent = regularStudent;
                    fullPath = Path.Combine(directoryPath, "Regular Student Details.txt");
                    allLines = File.ReadAllLines(fullPath);
                }
                else if (student is ExchangeStudent exchangeStudent)
                {
                    selectedStudent = exchangeStudent;
                    fullPath = Path.Combine(directoryPath, "Exchange Student Details.txt");
                    allLines = File.ReadAllLines(fullPath);
                }
                try
                {
                    if (allLines != null && selectedStudent != null)
                    {
                        int indexOfLine = 0;
                        foreach (var line in allLines)
                        {
                            string[] contents = line.Split(',');
                            int.TryParse(contents[0], out int studentId);
                            if (studentId == selectedStudent.StudentID)
                            {
                                indexOfLine = allLines.IndexOf(line);
                                break;
                                //Console.WriteLine(indexOfLine);
                            }
                        }
                        using (StreamWriter writer = new StreamWriter(fullPath))
                        {
                            for (int line = 0; line < allLines.Length; line++)
                            {
                                if (line == indexOfLine)
                                {
                                    continue;
                                }
                                writer.WriteLine(allLines[line]);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogError(e);
                    Console.WriteLine("Unable to remove the student details from the list...");
                }
            }
            catch(Exception e)
            {
                LogError(e);
                Console.WriteLine("Unable to work with file after remove the data from the list..");
            }
        }

        //update the student details in a file
        public void UpdateStudentDetailsInFile(Student student)
        {
            try
            {
                string[] allLines = null;
                string fullPath = "";
                if (student is RegularStudent regularStudent)
                {
                    //update a data from a file
                    fullPath = Path.Combine(directoryPath, "Regular Student Details.txt");
                    allLines = File.ReadAllLines(fullPath);

                    int indexOfLine = 0;
                    foreach (string line in allLines)
                    {
                        string[] contents = line.Split(',');
                        int.TryParse(contents[0], out int studentId);
                        if (studentId == regularStudent.StudentID)
                        {
                            indexOfLine = allLines.IndexOf(line);
                            break;
                        }
                    }
                    for (int line = 0; line < allLines.Length; line++)
                    {
                        if (line == indexOfLine)
                        {
                            allLines[line] = $"{regularStudent.StudentID},{regularStudent.StudentName},{regularStudent.Marks[0]}:{regularStudent.Marks[1]}:{regularStudent.Marks[2]}:{regularStudent.Marks[3]}:" +
                                $"{regularStudent.Marks[4]},{regularStudent.Grade}";
                        }
                    }
                    File.WriteAllLines(fullPath, allLines);
                }
                else if (student is ExchangeStudent exchangeStudent)
                {
                    fullPath = Path.Combine(directoryPath, "Exchange Student Details.txt");
                    allLines = File.ReadAllLines(fullPath);

                    int indexOfLine = 0;
                    foreach (string line in allLines)
                    {
                        string[] contents = line.Split(',');
                        int.TryParse(contents[0], out int studentId);
                        if (studentId == exchangeStudent.StudentID)
                        {
                            indexOfLine = allLines.IndexOf(line);
                            break;
                        }
                    }
                    for (int line = 0; line < allLines.Length; line++)
                    {
                        if (line == indexOfLine)
                        {
                            allLines[line] = $"{exchangeStudent.StudentID},{exchangeStudent.StudentName},{exchangeStudent.Marks[0]}:{exchangeStudent.Marks[1]}:{exchangeStudent.Marks[2]}:{exchangeStudent.Marks[3]}:" +
                                $"{exchangeStudent.Marks[4]},{exchangeStudent.Grade}";
                        }
                    }
                    File.WriteAllLines(fullPath, allLines);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("unable to update a student details in a file..");
            }
        }
           

        /// <summary>
        /// Get option from the user for the switch case
        /// </summary>
        /// <returns> integer value from 1 to 7 </returns>
        public int GetOption()
        {
            try
            {
                string option = Console.ReadLine();
                int choice;
                while (!int.TryParse(option, out choice) || choice < 0 || choice > 7)
                {
                    Console.Write("Enter a valid choice (1-7) : ");
                    option = Console.ReadLine();
                }
                return choice;
            }
            catch(Exception e)
            {
                Console.WriteLine("problem while getting option for the operation.");
                LogError(e);
                return 0;
            }
        }


        /// <summary>
        /// Add a new student to the list of students based on the user input
        /// </summary>
        public void AddStudent()
        {
            try
            {
                int studentId = GetStudentId();
                while (students.Any(Student => Student.StudentID == studentId))
                {
                    Console.WriteLine("The Student id already exists : ");
                    studentId = GetStudentId();
                }

                string studendName = GetStudentName();
                double[] marks = GetStudentMarks(subjects);
                double averageMark = GetAverageMark(marks);
                Console.WriteLine("The average mark you got is : " + averageMark);
                string selectStudent = GetWhichStudent();
                if (selectStudent == "r")
                {
                    string grade = GetGrade(averageMark);
                    students.Add(new RegularStudent(studentId, studendName, marks, grade));
                    Console.WriteLine("your data added to the list....");
                    //store the student details into the text file
                    using (StreamWriter writer = new StreamWriter(Path.Combine(directoryPath, "Regular Student Details.txt"), true))
                    {
                        writer.Write($"{studentId},{studendName},");
                        int i = 0;
                        foreach (double mark in marks)
                        {
                            writer.Write($"{mark}");
                            if (i < marks.Length - 1)
                            {
                                i++;
                                writer.Write(":");
                            }
                        }
                        writer.WriteLine($",{grade}");
                    }
                }
                else if (selectStudent == "e")
                {
                    string grade = GetGradeForExchangeStudent(marks);
                    students.Add(new ExchangeStudent(studentId, studendName, marks, grade));
                    Console.WriteLine("your data added to the list....");
                    using (StreamWriter writer = new StreamWriter(Path.Combine(directoryPath, "Exchange Student Details.txt"), true))
                    {
                        writer.Write($"{studentId},{studendName},");
                        int i = 0;
                        foreach (double mark in marks)
                        {
                            writer.Write($"{mark}");
                            if (i < marks.Length - 1)
                            {
                                i++;
                                writer.Write(":");
                            }

                        }
                        writer.WriteLine($",{grade}");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("unable to add a student details to the list");
                LogError(e);
            }
        }

        /// <summary>
        /// update a user marks by get student id
        /// </summary>
        public void UpdateStudent()
        {
            try { 
            int studentId;
            Console.WriteLine("Do you know the student ID ?");
            string proceed = GetYesOrNoForProceed();
            if (proceed == "y")
            {
                Console.Write("Enter the student id that you want to update : ");
                studentId = GetStudentId();
                while (!students.Any(Student => Student.StudentID == studentId))
                {
                    Console.WriteLine("The Student id not found in the list of students  : ");
                    studentId = GetStudentId();
                }

                if (students.Any(Student => Student.StudentID == studentId))
                {
                    try
                    {
                        var studentToUpdate = students.FirstOrDefault(student => student.StudentID == studentId);
                        studentToUpdate.DisplayStudentDetails(subjects);

                        Console.WriteLine("Do you want to change the name of this student?");
                        string confirm = GetYesOrNoForProceed();
                        if (confirm == "y") studentToUpdate.StudentName = GetStudentName();
                        else Console.WriteLine("The name is not changed!...");


                        int i = 0;
                        foreach (var subject in subjects)
                        {
                            Console.Write($"Enter new mark in {subject} : ");
                            double mark = GetMarkPerSubject();
                            studentToUpdate.Marks[i] = mark;
                            i++;
                        }
                        if(studentToUpdate is RegularStudent regularStudent)
                        {
                            double averageMark = GetAverageMark(regularStudent.Marks);
                            regularStudent.Grade = GetGrade(averageMark);
                            Console.WriteLine("Updated student details : ");
                            regularStudent.DisplayStudentDetails(subjects);
                            UpdateStudentDetailsInFile(regularStudent);
                        }
                        else if(studentToUpdate is ExchangeStudent exchangeStudent)
                        {
                            double averageMark = GetAverageMark(exchangeStudent.Marks);
                            exchangeStudent.Grade = GetGradeForExchangeStudent(exchangeStudent.Marks);
                            Console.WriteLine("Updated student details : ");
                            exchangeStudent.DisplayStudentDetails(subjects);
                            UpdateStudentDetailsInFile(exchangeStudent);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine($"The student with this {studentId} is not in the list..");
                    }
                }
            }
            else
            {
                Console.Write("Enter the student name that you want to update : ");
                string studentName = GetStudentName();
                while (!students.Any(Student => Student.StudentName == studentName))
                {
                    Console.WriteLine("The Student name is not found in the list of students  : ");
                    studentName = GetStudentName();
                }
                if (students.Any(Student => Student.StudentName == studentName))
                {
                    try
                    {
                        var studentToUpdate = students.FirstOrDefault(student => student.StudentName == studentName);

                        studentToUpdate.DisplayStudentDetails(subjects);
                        int i = 0;
                        foreach (var subject in subjects)
                        {
                            Console.Write($"Enter new mark in {subject} : ");
                            double mark = GetMarkPerSubject();
                            studentToUpdate.Marks[i] = mark;
                            i++;
                        }
                        if (studentToUpdate is RegularStudent regularStudent)
                        {
                            double averageMark = GetAverageMark(regularStudent.Marks);
                            regularStudent.Grade = GetGrade(averageMark);
                            Console.WriteLine("Updated student details : ");
                            regularStudent.DisplayStudentDetails(subjects);
                            UpdateStudentDetailsInFile(regularStudent);
                        }
                        else if (studentToUpdate is ExchangeStudent exchangeStudent)
                        {
                            double averageMark = GetAverageMark(exchangeStudent.Marks);
                            exchangeStudent.Grade = GetGradeForExchangeStudent(exchangeStudent.Marks);
                            Console.WriteLine("Updated student details : ");
                            exchangeStudent.DisplayStudentDetails(subjects);
                            UpdateStudentDetailsInFile(exchangeStudent);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine($"The student with this {studentName} is not in the list..");
                    }
                }
            }
        }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to update a student details");
                LogError(e);
    }
}


        /// <summary>
        /// Remove the student from the students list by get id from the user
        /// </summary>
        public void RemoveStudent()
        {
            try
            {
                Console.Write("Enter the student id that you want to remove : ");
                int studentId = GetStudentId();
                while (!students.Any(Student => Student.StudentID == studentId))
                {
                    Console.WriteLine("The Student id not found in the list of students  : ");
                    studentId = GetStudentId();
                }

                if (students.Any(Student => Student.StudentID == studentId))
                {
                    try
                    {
                        var studentToRemove = students.FirstOrDefault(student => student.StudentID == studentId);
                        Console.WriteLine($"Do you want to remove this student {studentToRemove.StudentID} - {studentToRemove.StudentName}");
                        string proceed = GetYesOrNoForProceed();
                        if (proceed == "y")
                        {
                            UpdateRemovedStudentInFile(studentToRemove);  // for try to update the file
                            students.Remove(studentToRemove);
                            Console.WriteLine($"The student with the id \"{studentId}\" is removed from the list ");
                        }
                        else
                            Console.WriteLine("The student details is not removed from the list ! ");
                    }
                    catch (Exception e)
                    {
                        LogError(e);
                        Console.WriteLine(e.Message);
                        Console.WriteLine($"The student with this {studentId} is not in the list..");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("unable to remove a student details from the list");
                LogError(e);
            }
}

        /// <summary>
        /// Display student Details in the model classes based on the type the method get called and done the method overriding (polymorphism)
        /// </summary>
        public void DisplayStudentDetails()
        {
            try
            {
                if (students.Count != 0)
                {
                    var sortedStudents = students.OrderBy(student => student.StudentID);
                    Console.WriteLine("\n---------------------------------------------------------------------------------------------------\n");
                    Console.Write($"\t{"Student ID",-12}{"Name",-18}");
                    foreach (var subject in subjects)
                    {
                        Console.Write($"{subject,-10}");
                    }
                    Console.WriteLine($"{"Grade",-10}");
                    foreach (var student in sortedStudents)
                    {
                        Console.WriteLine("\n---------------------------------------------------------------------------------------------------\n");
                        if (student is RegularStudent regularStudent)
                        {
                            regularStudent.DisplayStudentDetails(subjects);
                        }
                        else if (student is ExchangeStudent exchangeStudent)
                        {
                            exchangeStudent.DisplayStudentDetails(subjects);
                        }
                    }
                    Console.WriteLine("\n---------------------------------------------------------------------------------------------------\n");
                }
                else
                {
                    Console.WriteLine("The list is empty so, add student details by select option 1 !..");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error :  unable to display student details");
            }
        }

        /// <summary>
        /// Display a particular student details using student Id 
        /// </summary>
        public void DisplayOneStudentDetails()
        {
            try
            {
                Console.WriteLine("Enter the student Id or Name : ");
                string inputStudentNameOrId = Console.ReadLine();
                bool isSucess = int.TryParse(inputStudentNameOrId, out int studentIdOrName);
                if (isSucess)
                {
                    //Console.Write("Enter the student id that you want to see : ");
                    //int studentId = GetStudentId();
                    while (!students.Any(Student => Student.StudentID == studentIdOrName))
                    {
                        Console.WriteLine("The Student id  not found in the list of students  : ");
                        studentIdOrName = GetStudentId();
                    }
                    if (students.Any(Student => Student.StudentID == studentIdOrName))
                    {
                        Console.WriteLine("\n-------------------------------------------------------------------------------------------\n");
                        try
                        {
                            var studentToDisplay = students.FirstOrDefault(student => student.StudentID == studentIdOrName);
                            Console.Write($"\t{"Student ID",-12}{"Name",-18}");
                            foreach (var subject in subjects)
                            {
                                Console.Write($"{subject,-10}");
                            }
                            Console.WriteLine($"{"Grade",-10}");
                            studentToDisplay.DisplayStudentDetails(subjects);
                        }
                        catch (Exception e)
                        {
                            LogError(e);
                            Console.WriteLine(e.Message);
                            Console.WriteLine($"The student with this {studentIdOrName} is not in the list..");
                        }
                        Console.WriteLine("\n-------------------------------------------------------------------------------------------\n");
                    }

                }
                else
                {
                    var matchedStudents = students.Where(student => student.StudentName.Equals(inputStudentNameOrId));
                    Console.WriteLine("These are the students with the given name : ");

                    foreach (var matchedStudent in matchedStudents)
                    {
                        if (matchedStudent is RegularStudent regularStudent)
                        {
                            Console.WriteLine("\n-------------------------------------------------------------------------------------------\n");
                            Console.Write($"\t{"Student ID",-12}{"Name",-18}");
                            foreach (var subject in subjects)
                            {
                                Console.Write($"{subject,-10}");
                            }
                            Console.WriteLine($"{"Grade",-10}");
                            regularStudent.DisplayStudentDetails(subjects);
                            Console.WriteLine("\n-------------------------------------------------------------------------------------------\n");
                        }
                        else if (matchedStudent is ExchangeStudent exchangeStudent)
                        {
                            Console.WriteLine("\n-------------------------------------------------------------------------------------------\n");
                            Console.Write($"\t{"Student ID",-12}{"Name",-18}");
                            foreach (var subject in subjects)
                            {
                                Console.Write($"{subject,-10}");
                            }
                            Console.WriteLine($"{"Grade",-10}");
                            exchangeStudent.DisplayStudentDetails(subjects);
                            Console.WriteLine("\n-------------------------------------------------------------------------------------------\n");
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to fetch the student detail from the list");
                LogError(e);
            }
        }


        /// <summary>
        /// Group the students based on their grades
        /// </summary>
        public void GroupStudentsBasedOnGrade()
        {
            try
            {
                List<RegularStudent> regularStudents = new List<RegularStudent>();
                List<ExchangeStudent> exchangeStudents = new List<ExchangeStudent>();

                foreach (var student in students)
                {
                    if (student is RegularStudent regularStudent)
                        regularStudents.Add(regularStudent);
                    else if (student is ExchangeStudent exchangeStudent)
                        exchangeStudents.Add(exchangeStudent);
                }

                var groupedRegularStudents = regularStudents.GroupBy(student => student.Grade);
                var groupedExchangeStudets = exchangeStudents.GroupBy(student => student.Grade);

                Console.WriteLine("REGULAR STUDENTS");
                foreach (var group in groupedRegularStudents)
                {
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n");
                    Console.WriteLine($"GRADE : {group.Key}\nNumber of students in this Grade : {group.Count()}");
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n");

                    foreach (var student in group)
                    {
                        Console.WriteLine($"Student name : {student.StudentName} ");

                    }
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n");
                }
                Console.WriteLine("EXCHANGE STUDENTS");
                foreach (var group in groupedExchangeStudets)
                {
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n");
                    Console.WriteLine($"GRADE : {group.Key}\nNumber of students in this Grade : {group.Count()}");
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n");

                    foreach (var student in group)
                    {
                        Console.WriteLine($"Student name : {student.StudentName} ");

                    }
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n");
                }
            }
           catch(Exception e)
            {
                Console.WriteLine("Error : unable to group the students in the list..");
            }
        }

        //get student id and return it
        public int GetStudentId()
        {
            try
            {
                Console.Write("Enter the student id : ");
                string inputId = Console.ReadLine();
                int studentId;
                while (!int.TryParse(inputId, out studentId))
                {
                    Console.Write("Enter a valid student Id : ");
                    inputId = Console.ReadLine();
                }
                return studentId;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error :unable to get the id from the user");
                LogError(e);
                return -1;
            }
        }

        //get student name and return it 
        public string GetStudentName()
        {
            try
            {
                Console.Write("Enter the student name : ");
                string studentName = Console.ReadLine();
                while (!(!int.TryParse(studentName, out _) && !string.IsNullOrWhiteSpace(studentName) && studentName.Length > 2 && studentName.All(c => Char.IsLetter(c) || c == '_' || c == ' ')))
                {
                    Console.Write("Enter a valid name : ");
                    studentName = Console.ReadLine();
                }
                return studentName;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get the student name from the user..");
                LogError(e);
                return null;
            }
        }

        //get students mark for each subject and store it in array and return it
        public double[] GetStudentMarks(List<string> subjects)
        {
            try
            {
                double[] marks = new double[subjects.Count];
                int i = 0;
                foreach (string subject in subjects)
                {
                    Console.Write($"Enter the mark in {subject} : ");
                    double mark = GetMarkPerSubject();
                    marks[i] = mark;
                    i++;
                }
                return marks;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get the marks from the user");
                LogError(e);
                return null;
            }
        }

        //get mark for each subject and return it to the GetStudentMarks() method
        public double GetMarkPerSubject()
        {
            try
            {
                double mark;
                string inputMark = Console.ReadLine();
                while (!double.TryParse(inputMark, out mark) || mark < 0 || mark > markPerSubject)
                {
                    Console.Write("Enter a valid mark : ");
                    inputMark = Console.ReadLine();
                }
                return mark;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get the one subject  mark fromt he user");
                LogError(e);
                return -1;
            }
        }

        //use the marks array to caluculate the average and return it 
        public double GetAverageMark(double[] marks)
        {
            try
            {
                double total = 0;
                foreach (var mark in marks)
                {
                    total += mark;
                }
                return total / marks.Length;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get average for the given double array date");
                LogError(e);
                return -1;
            }
        }

        //return the grade of the student based on their average mark
        public string GetGrade(double averageMark)
        {
            string grade;
            if (averageMark >= 90 && averageMark <= 100)
                return "A+";
            else if (averageMark < 90 && averageMark >= 80)
                return "A";
            else if (averageMark < 80 && averageMark >= 70)
                return "B";
            else if (averageMark < 70 && averageMark >= 60)
                return "C";
            else if (averageMark < 60 && averageMark >= 50)
                return "D";
            else
                return "F";
        }

        //get grade for the exchange student
        public string GetGradeForExchangeStudent(double[] marks)
        {
            try
            {
                string grade;
                double averagePercentage = (marks.Sum() / (marks.Length * markPerSubject)) * 100;
                Console.WriteLine(averagePercentage);
                return averagePercentage > 60 ? "pass" : "fail";
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to calculate grade for the exchange students..");
                LogError(e);
                return null;
            }
        }

        //Get yes or no to confirm  which student instance have to create
        public string GetWhichStudent()
        {
            try
            {
                string option;
                do
                {
                    Console.WriteLine("\tEnter \"r\" for the Regular student\n\t\tOR\n\tEnter \"e\" for the exchange student ");
                    option = Console.ReadLine().Trim().ToLower();
                } while (option != "r" && option != "e");
                return option;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get which student type ");
                LogError(e);
                return null;
            }
        }

        //used to confirm before deletion or updation of user details
        public string GetYesOrNoForProceed()
        {
            try
            {
                string option;
                do
                {
                    Console.Write("\tEnter \"y\" or \"n\" : ");
                    option = Console.ReadLine().Trim().ToLower();
                } while (option != "y" && option != "n");
                return option;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get yes or no from the user..");
                LogError(e);
                return null;
            }
        }


        //for log the errors in the text file 
        public void LogError(Exception e)
        {
            try
            {
                string logDirecotryPath = "D:\\Azure-Assignment\\C#-Tasks\\C#-Tasks\\ConsoleApplication\\SchoolManagementSystem\\Error log files\\";
                using (StreamWriter writer = new StreamWriter(Path.Combine(logDirecotryPath, $"Error_log_{DateTime.Now.ToString("dd-MM-yyyy")}.txt"), true))
                {
                    writer.WriteLine("\n==========================================================================================================================\n");
                    writer.WriteLine($"Source : {e.Source} ");
                    writer.WriteLine($"Error : {e.Message}");
                    writer.WriteLine($"StackTrace : \n{e.StackTrace}");
                    writer.WriteLine($"Date and Time : {DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}");
                    writer.WriteLine("\n==========================================================================================================================\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error : unable to log the error details to the file.");
            }
        }
    }
}
