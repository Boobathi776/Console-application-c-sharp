

namespace Tasks.Practice
{
    internal class DateTimeCheck
    {
        private const string format = "yyyy-MM-dd";

        public DateTimeCheck() 
        { 

        }

        public void ShowCurrentTime()
        {
            ////DateTime datetime = DateTime.Now;
            //Console.WriteLine(DateTime.Now);
            //Console.WriteLine(DateTime.UtcNow);
            //Console.WriteLine(DateTime.Today);

            ////for store the manual date format is  yyyy - MM - dd HH-mm-ss
            //DateTime dt = new DateTime(2025, 10, 2, 12, 40, 0);
            //Console.WriteLine(dt);

            ////for used to store a time period 
            //TimeSpan span = new TimeSpan(2, 30, 0);
            //Console.WriteLine(span);

            //Console.WriteLine(span.TotalMinutes);  // to get the totalminutes

            ////Add or reduce hours 
            //DateTime start = DateTime.Now;
            //DateTime end = start.AddHours(10);
            //TimeSpan diff = end - start;
            //Console.WriteLine(diff);

            ////reduce hours
            //end = start.AddHours(-7);
            //Console.WriteLine(end);

            ////use to string to change the format 
            ////if we need time only or date only we can print that only 
            //Console.WriteLine(dt.ToString("dd-MM-yyyy  HH-mm-ss"));

            ////parse the date and time 
            //DateTime date2 = DateTime.Parse("05-05-2006");
            //if (DateTime.TryParse(date2.ToString(), out date2))
            //    Console.WriteLine(date2.ToString());
            //else
            //    Console.WriteLine("not success");
            //DateTime date3 = DateTime.ParseExact("06-05-2025", "dd-MM-yyyy", null);
            //Console.WriteLine(date3.ToString());

            ////for only take date from the datetime same for TimeOnly
            //DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            //Console.WriteLine(date);

            string date = Console.ReadLine();
            DateOnly value;
            bool correct = DateOnly.TryParse(date,out  value);
            if (correct)
            {
                Console.WriteLine(value);   
            }
            else
            {
                Console.WriteLine("Enter a valid date : ");
            }


            
        }
    }
}
