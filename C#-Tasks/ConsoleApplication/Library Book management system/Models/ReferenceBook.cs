using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.ConsoleApplication.Library_Book_management_system.Models
{
    internal class ReferenceBook : Book
    {
        private readonly string IsBorrowed = "--";

            public ReferenceBook(int bookId, string bookTitle, string bookAuthor, int publicationYear)
            {
                BookId = bookId;
                BookTitle = bookTitle;
                Author = bookAuthor;
                PublicationYear = publicationYear;
            }

        public override void DisplayBookDetails()
        {        
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\t{BookId,-10}{BookTitle,-15}{Author,-15}{PublicationYear,-18}{IsBorrowed,-15}");
                Console.ResetColor();
        }
    }
    
}
