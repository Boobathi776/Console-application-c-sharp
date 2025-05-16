using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.StyledXmlParser.Node;

namespace Tasks.ConsoleApplication.Library_Book_management_system.Models
{
    internal class LendingBook : Book
    {
        public string Availability { get; set; }
        public bool IsBorrowed { get; set; } 
        public LendingBook(int bookId, string bookTitle,string bookAuthor,int publicationYear, bool isBorrowed = false)
        {
            BookId = bookId;
            BookTitle = bookTitle;
            Author = bookAuthor;
            PublicationYear = publicationYear;
            if (isBorrowed)
            {
                Availability = "Not Available";
            }
            else
            {
                Availability = "Available";
            }
           
        }

        public override void DisplayBookDetails()
        {
            if (Availability == "Not Available")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\t{BookId,-10}{BookTitle,-15}{Author,-15}{PublicationYear,-18}{Availability,-15}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\t{BookId,-10}{BookTitle,-15}{Author,-15}{PublicationYear,-18}{Availability,-15}");
                Console.ResetColor();
            }
               
        }
        }
    
}

