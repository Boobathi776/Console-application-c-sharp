using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.ConsoleApplication.Library_Book_management_system.Interfaces
{
    public interface IBook
    {
        int BookId { get; init; }
        string BookTitle { get; set; }
        string Author { get; set; }
        int PublicationYear { get; set; }

        public void DisplayBookDetails();

    }
}
