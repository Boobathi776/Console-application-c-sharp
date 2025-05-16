
namespace Tasks;

class StudentReport
{
    public void assignGrade()
    {
        string studentName;
        int subject_count = 5;
        double total = 0, mark = 0;
        double average = 0;
        char choice, grade;

        Console.WriteLine(" Student Marks Report");
        do
        {
            Console.Write("Enter your name : ");
            studentName = Console.ReadLine();
           
            for (int i = 0; i < subject_count; i++)
            {
                Console.Write($"Enter the mark for subject {i + 1} = ");
                bool correct = double.TryParse(Console.ReadLine(), out mark);
                if (mark <= 100 && correct) total += mark;
                else
                {
                    Console.WriteLine("please enter a valid mark");
                    Console.Write($"Enter the mark for subject {i+1} = ");
                    double.TryParse(Console.ReadLine(), out mark);
                    total += mark;
                }
            }

            if (total < 500)
                average = total / subject_count;
            if (average >= 90) grade = 'A';
            else if (average >= 75) grade = 'B';
            else if (average >= 60) grade = 'C';
            else grade = 'F';

            Console.WriteLine($" Name : {studentName} \n Total marks : {total} \n Average score : {average} \n Grade : {grade}");
            Console.WriteLine("==============================================================");
            total = 0;
            average = 0;
            Console.Write("If still studented left in your classroom then click 'y' or click 'n' : ");
            char.TryParse(Console.ReadLine(), out choice);
        } while (choice != 'n');

    }
}