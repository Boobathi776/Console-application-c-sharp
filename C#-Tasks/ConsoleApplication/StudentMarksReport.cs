namespace Tasks.ConsoleApplication;

class Student
{
    public string StudentName;
  
    public double TotalMarks { get; set; }
    public double[] SubjectMark {  get; set; }
    public char Grade {  get; set; }
    public Student(string studentName,double[] subjectMark,double totalMarks,char grade)
    {
        StudentName = studentName;
        SubjectMark = subjectMark;
        TotalMarks = totalMarks;
        Grade = grade;
    }
}
internal class StudentMarksReport
{
    List<string> subjects = new List<string>() { "Tamil","English","Maths","Science","Social Science"};
    SortedDictionary<int, Student> studentDetails = new SortedDictionary<int, Student>();
    readonly int MarkPerSubject;
    public StudentMarksReport(int markPerSubject)
    {
        MarkPerSubject = markPerSubject;
    }
    
    //The exexution starting function
    public void StudentMarkSystem()
    {
        Console.WriteLine("Student Mark Report System");
        StoreStudentData(); 
        ShowStudentDetails();
        Console.Write("Do you want to update student details enter (y/n) : ");
    GetChoice:
        string yes = Console.ReadLine();
        if (yes.ToLower() == "y" && yes.ToLower() != "n")
        {
            UpdateStudentDetails();
            Console.WriteLine("Thank you............!");
        }
        else if (yes.ToLower() == "n") { }
        else
        {
            Console.Write("Enter a valid choice : ");
            goto GetChoice;
        }
    }

    //Get input from user and store it in a dictionary by making object for each student
    public void StoreStudentData()
    {
        string proceed;
        int studentId;
        double[] marks = new double[subjects.Count];
        do
        {
            Console.Write("Enter a student ID : ");
            studentId = GetStudentID();
            Console.Write("Enter student name : ");
        GetStudentName:
            string studentName = Console.ReadLine();
            if (studentName.ToUpper().Length != 0)
            {
                Console.WriteLine();
                int i = 0;
                foreach (var subject in subjects)
                {
                    Console.Write($"Enter the mark in {subject} : ");
                    marks[i] = GetMark();
                    i++;
                }
            }
            else
            {
                Console.Write("Enter a valid name : ");
                goto GetStudentName;
            }

            Console.WriteLine($"Total mark is {marks.Sum()}\n");
            
            double totalMarks = marks.Length * MarkPerSubject;
            if (marks.Sum() <= totalMarks)
            {
                double averageMark = marks.Average();
                Console.WriteLine($"Aveage mark is {averageMark}\n");
                char grade = GetGrade(averageMark);
                Console.WriteLine($"Your grade is : {grade}\n");
                studentDetails.Add( studentId , new Student(studentName,marks,marks.Sum(),grade));
                totalMarks = 0;
                averageMark = 0;
            }
            else
            {
                Console.WriteLine("The total of the marks that student get exceeds the Total Marks");
            }
                Console.Write("Do you have any other studets left in the classroom (y/n) : ");
        GetOptionAgain:
            proceed = Console.ReadLine().Trim()??"Z";
            if (proceed.ToLower() == "n")
            {
                ShowStudentDetails();
                UpdateStudentDetails();
                Console.WriteLine("Exiting.......");
                break;
            }
            else if(proceed.ToLower() != "y")
            {
                Console.Write("Enter a valid option (y/n) : ");
                goto GetOptionAgain;
            }
        } while (proceed.ToLower() == "y");
    }

    // show the student details to the user
    private void ShowStudentDetails()
    {
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        Console.WriteLine("StudentID\tStudent Name\tTotal Marks\tGrade");
        Console.WriteLine("-------------------------------------------------------------------------------------------");

        foreach (var student in studentDetails)
        {
            //if (studentDetails.ContainsKey(student.Key))
            {
                Console.WriteLine($"{student.Key}\t\t{student.Value.StudentName}\t\t{student.Value.TotalMarks}\t\t{student.Value.Grade}");
            }
        }
    }

    //Get mark for each subject from the user
    private  double GetMark()
    {
        double mark;
        while (true)
        {
            bool isSuccess = double.TryParse(Console.ReadLine(), out mark);
            int perSubjectMark = 100;
            if (isSuccess && mark <= perSubjectMark)
            {
                return mark;
            }
            else
            {
                Console.Write("Enter a valid mark : ");
            }
        }
    }

    //Get the student id and that should be unique
    private int GetStudentID()
    {
        int studentId;
        while (true)
        {
            bool isSuccess = int.TryParse(Console.ReadLine(), out studentId);
            if(studentDetails.ContainsKey(studentId))
            {
                Console.Write("The ID is already available so enter unique ID : ");
            }
            else if (isSuccess && studentId > 0 && !studentDetails.ContainsKey(studentId))
            {
                return studentId;
            }
            else
            {
                Console.Write("Enter a valid Student ID : ");
            }
        }
    }

    //Validate the Grade based on the average score of the student marks
    private char GetGrade(double averageMark)
    {
        char grade;
            if (averageMark >= 90)
            {
                return grade = 'A';
            }
            else if (averageMark >= 75)
            {
                return grade = 'B';
            }
            else if (averageMark >= 60)
            {
                return grade = 'C';
            }
            else
            {
                return grade = 'F';
            }
    }

    //For Get the preseneted id from the studentDetails
    private int GetStudentIDForUpdate()
    {
        int studentId;
        while (true)
        {
            bool isSuccess = int.TryParse(Console.ReadLine(), out studentId);
            if (isSuccess && studentId > 0 && studentDetails.ContainsKey(studentId))
            {
                return studentId;
            }
            else
            {
                Console.Write("Enter a valid Student ID : ");
            }
        }
    }


    //Update the student details 
    private void UpdateStudentDetails()
    {
        int option;
        do
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\t1.Add student Detail\n\t2.Remove student detail\n\t3.Update Student mark \n\t4.Save \n\t0.Exit");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.Write("Enter your choice : ");
        GetChoice:
            int.TryParse(Console.ReadLine(), out option);
            if (option == 1)
            {
                StoreStudentData();
                break;
            }
            else if (option == 2)
            {
               
                Console.Write("Enter the Student ID that you want to remove : ");
                int studentId = GetStudentIDForUpdate();
                
                if (studentDetails.ContainsKey(studentId) )
                {
                    Console.WriteLine($"The student with ID {studentId}-{studentDetails[studentId].StudentName} is removed from the Database.");
                    studentDetails.Remove(studentId);
                }
                else
                {
                    Console.WriteLine($"The product with ID {studentId} is not found in the Dataset...");
                }
            }
            else if (option == 3)
            {
                Console.Write("Enter the Student ID that you want to update : ");
                int studentId = GetStudentIDForUpdate();
                Console.WriteLine("Enter the subject mark that you want to update or just enter the old mark ...");
               if (studentDetails.ContainsKey(studentId))
                {
                    int i = 0;
                    double totalMarks = 0;
                    foreach (var subject in subjects)
                    {
                        Console.WriteLine($"Old {subject} Mark : {studentDetails[studentId].SubjectMark[i]}");
                        Console.Write($"Enter New Mark in {subject} :");
                        double changedMark = GetMark();
                        studentDetails[studentId].SubjectMark[i] = changedMark;
                        totalMarks += changedMark;
                        i++;
                    }
                    studentDetails[studentId].TotalMarks = totalMarks;
                    char changedGrade = GetGrade(totalMarks / subjects.Count);
                    studentDetails[studentId].Grade = changedGrade;
                }
            }
            else if (option == 4)
            {
                Console.WriteLine("Your details are saved successfully..") ;
                Console.WriteLine("Updated student details ");
                ShowStudentDetails();
                break;
            }
            else if (option == 0) break;
            else
            {
                Console.Write("Enter a valid choice : ");
                goto GetChoice;
            }
        } while (option != 0);
    }
}
