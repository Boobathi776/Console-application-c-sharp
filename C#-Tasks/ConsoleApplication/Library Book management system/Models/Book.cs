using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.ConsoleApplication.Library_Book_management_system.Interfaces;

namespace Tasks.ConsoleApplication.Library_Book_management_system.Models
{
    internal class Book : IBook
    {
        public int BookId { get; init; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public virtual void DisplayBookDetails()
        {
            Console.WriteLine($"\t{BookId,-10}{BookTitle,-15}{Author,-15}{PublicationYear,-15}");
        }

    }
}
