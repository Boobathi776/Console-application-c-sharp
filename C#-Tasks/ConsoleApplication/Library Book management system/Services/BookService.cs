using System;
using System.Collections.Generic;
using System.Linq;
using iText.Forms.Fields.Merging;
using iText.StyledXmlParser.Css.Selector.Item;
using Tasks.ConsoleApplication.Library_Book_management_system.Interfaces;
using Tasks.ConsoleApplication.Library_Book_management_system.Models;
namespace Tasks.ConsoleApplication.Library_Book_management_system.Services
{
    internal class BookService
    {
        List<Book> books = new List<Book>()
        {
            new ReferenceBook(1,"Java","Schild",2004),
            new LendingBook(2,"Atomic habits","James clearer",2012,false),
            new LendingBook(3,"Jurassic park","Tom",2010,false),
            new LendingBook(4,"daily habits","Jhoseph",2008,false),
            new ReferenceBook(5,"C#","someone", 1995),
            new LendingBook(6,"Weekly habits","Jhoseph",2012,false),
            new ReferenceBook(7,"daily Routine","someone", 2004),
            new LendingBook(8,"Jurassic world ","sony pix",1008,false),

        };

        public void Librarian()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n*************************************************************************************************\n");
                Console.WriteLine("***************************         Library Management System        ****************************");
                Console.WriteLine("\n*************************************************************************************************\n");
                Console.ResetColor();
                DisplayBookDetails();
                int choice;
                do
                {
                    Console.WriteLine("\t1. Add Book\n\t2. Update Book Details\n\t3. Remove Book\n\t4. Display Book Details\n\t5. Search Book\n\t6. Borrow book\n\t7. Return Book\n\t8. Exit");
                    choice = GetOption();
                    switch (choice)
                    {
                        case 1:
                            AddBook();
                            break;
                        case 2:
                            UpdateBookDetails();
                            break;
                        case 3:
                            RemoveBook();
                            break;
                        case 4:
                            DisplayBookDetails();
                            break;
                        case 5:
                            SearchBook();
                            break;
                        case 6:
                            BorrowBook();
                            break;
                        case 7:
                            ReturnBook();
                            break;
                        case 8:
                            Console.WriteLine("Exiting.......");
                            break;
                        default:
                            Console.WriteLine("Enter a valid option......");
                            break;
                    }
                } while (choice != 8);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to done one of the process in librarian method..");
                LogError(e);
            }
        }

        public void AddBook()
        {
            try
            {
                int bookId = GetBookId();
                while (books.Any(book => book.BookId == bookId))
                {
                    Console.WriteLine($"\tThe book with this id {bookId} already exist ! so enter a unique id for the book!");
                    bookId = GetBookId();
                }
                string bookTitle = GetBookName();
                string bookAuthor = GetAuthorName();
                int year = GetPublishedYear();
                string selectedBookType = GetBookType();
                if (bookId == 0 || bookTitle == null || bookAuthor == null || year == 0 || selectedBookType == null)
                {
                    AddBook(); // start from again if any of the value is not proper
                }
                else if (selectedBookType == "r")
                {
                    books.Add(new ReferenceBook(bookId, bookTitle, bookAuthor, year));
                    Console.WriteLine("The book added successfully..");
                }
                else
                {
                    books.Add(new LendingBook(bookId, bookTitle, bookAuthor, year));
                }
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to add a book....!");

            }
        }

        public void UpdateBookDetails()
        {
            try
            {
                Book matchedBook = GetMatchingBook();
                if (matchedBook != null)
                {
                    Console.WriteLine($"\t{"Book ID",-10}{"Book Title",-15}{"Author",-15}{"Published Year",-18}{"Availability",-15}");
                    matchedBook.DisplayBookDetails();
                    Console.WriteLine("Enter new Details for the Book!");
                    Console.WriteLine("Do you want to change the book name ?");
                    int choice;
                    do
                    {
                        Console.WriteLine("1.Update Book Title\n2. Update Book Author\n3. Update Published Year\n4. Exit");
                        choice = GetChoice();
                        switch (choice)
                        {
                            case 1:
                                matchedBook.BookTitle = GetBookName();
                                Console.WriteLine("Title of the book updated");
                                break;
                            case 2:
                                matchedBook.Author = GetAuthorName();
                                Console.WriteLine("Author name is upated");
                                break;
                            case 3:
                                matchedBook.PublicationYear = GetPublishedYear();
                                Console.WriteLine("Published year updated");
                                break;
                            case 4:
                                Console.WriteLine("Exiting from the updating.....");
                                break;
                            default:
                                Console.WriteLine("Invalid choice : ");
                                break;
                        }
                    } while (choice != 4);
                }
                else
                {
                    Console.WriteLine("There is no book available with this name!");
                }
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to update book details");
                LogError(e);
            }
        }


        public void RemoveBook()
        {
            try
            {
                Book matchedBook = GetMatchingBook();
                if (matchedBook != null)
                {
                    Console.WriteLine($"Do you want to remove this book from the list  ID - {matchedBook.BookId} and tile : {matchedBook.BookTitle}?");
                    if (GetYesOrNoForProceed() == "y")
                    {
                        Console.WriteLine($"The book with the id {matchedBook.BookId} and tile : {matchedBook.BookTitle} is removed successfully!\n");
                        books.Remove(matchedBook);
                    }
                    else
                    {
                        Console.WriteLine("The book is not removed from the list..\n");
                    }
                }
                else
                {
                    Console.WriteLine("No book is available with the given id or name!\n");
                }
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");

            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to remove a book from the list....!");
            }
        }

        public void BorrowBook()
        {
            try
            {
                string bookIdOrTitle;
                do
                {
                    Console.Write("Enter a Book id or title : ");
                    bookIdOrTitle = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(bookIdOrTitle));
                int bookId;
                Book matchedBook;
                if (int.TryParse(bookIdOrTitle, out bookId))
                {
                    SearchByBookID(bookId);
                }
                else
                {
                    while (bookIdOrTitle.Length <= 1 || !books.Any(book => book.BookTitle.ToLower().Contains(bookIdOrTitle.ToLower())))
                    {
                        Console.WriteLine($"No book is available with this name {bookIdOrTitle}");
                        Console.Write("Enter a valid book name or enter Exit to stop : ");
                        bookIdOrTitle = Console.ReadLine();
                        if (bookIdOrTitle == "exit") break;
                    }
                    var matchedBooks = books.Where(book => book.BookTitle.ToLower().Contains(bookIdOrTitle.ToLower()));
                    if (matchedBooks != null)
                    {
                        Console.WriteLine("These are the books are available based on the given keyword : ");
                        Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
                        Console.WriteLine($"\t{"Book ID",-10}{"Book Title",-15}{"Author",-15}{"Published Year",-18}{"Availability",-15}");
                        Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
                        foreach (var book in matchedBooks)
                        {
                            if (book is LendingBook lendingBook)
                            {

                                lendingBook.DisplayBookDetails();
                            }
                            else if (book is ReferenceBook referenceBook)
                            {
                                referenceBook.DisplayBookDetails();
                            }
                            Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
                        }
                        Console.WriteLine("You can't take the books if they highlighted on Red color (because it's reference book )\n");
                        Console.WriteLine("Do you want any book from the above books ?");
                        string want = GetYesOrNoForProceed();
                        if (want == "y")
                        {
                            Console.WriteLine("Enter the book id that you want from the above list...");
                            bookId = GetBookId();
                            SearchByBookID(bookId);
                        }
                        else
                        {
                            Console.WriteLine("I think you don't want these books!");
                            Console.WriteLine("Do you want to search any other book ?");
                            string YesOrNo = GetYesOrNoForProceed();
                            if (YesOrNo == "y")
                            {
                                BorrowBook();
                            }
                            else
                            {
                                Console.WriteLine("I Hope you got your Book!\n");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No book is available with a given name!");
                    }
                }
                //Book book = GetMatchingBook();
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
            }
            catch(Exception e)
            {
                Console.WriteLine("Something went wrong while borrowing a book from the library...!");
                LogError(e);
            }
        }
        

        public void SearchByBookID(int bookId)
        {
            try
            {
                Book matchedBook;
                while (!books.Any(book => book.BookId == bookId))
                {
                    Console.WriteLine($"No book available with this id - {bookId}");
                    bookId = GetBookId();
                }
                matchedBook = books.FirstOrDefault(books => books.BookId == bookId);
                if (matchedBook != null)
                {
                    if (matchedBook is ReferenceBook referenceBook)
                    {
                        Console.WriteLine($"You can't borrow this book named {referenceBook.BookTitle}, because ,it's a reference book!");
                    }
                    else
                    {
                        Console.WriteLine($"Do you want to borrow this book with id :{matchedBook.BookId} and title : {matchedBook.BookTitle}?");
                        string proceed = GetYesOrNoForProceed();
                        if (proceed == "y")
                        {
                            if (matchedBook is LendingBook lendingBook)
                            {
                                lendingBook.IsBorrowed = true;
                                lendingBook.Availability = lendingBook.IsBorrowed ? "Not Available" : "Available";
                                Console.WriteLine("You successfully got your book from us. Hope you like this book!\nCome again.....\n");
                            }
                            else
                            {
                                Console.WriteLine("You doesn't borrow this book from us..");
                            }
                        }
                        else
                        {
                            Console.WriteLine("I think you don't want this book!");
                            Console.WriteLine("Do you want to search any other book ?");
                            string YesOrNo = GetYesOrNoForProceed();
                            if (YesOrNo == "y")
                            {
                                BorrowBook();
                            }
                            else
                            {
                                Console.WriteLine("I Hope you got your Book!!\n");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to process it with the id! ");
                LogError(ex);
            }
        }

        public void ReturnBook()
        {
            try
            {
                Book matchedBook = GetMatchingBook();
                if (matchedBook != null)
                {
                    if (matchedBook is LendingBook lendingBook && lendingBook.IsBorrowed == true)
                    {
                        lendingBook.IsBorrowed = false;
                        lendingBook.Availability = lendingBook.IsBorrowed ? "Not Available" : "Available";
                        Console.WriteLine($"The book that returned now is  Id : {lendingBook.BookId} and Title : {lendingBook.BookTitle}\n");
                    }
                    else
                    {
                        Console.WriteLine("Invalid book entry.....");
                    }
                }
                else
                {
                    Console.WriteLine("This book is not belongs to our library..");
                    throw new ArgumentNullException();
                }
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
            }
            catch(Exception e)
            {
                Console.WriteLine("unable to return a book..!");
                LogError(e);
            }
        }

        public void DisplayBookDetails()
        {
            try
            {
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
                Console.WriteLine($"\t{"Book ID",-10}{"Book Title",-15}{"Author",-15}{"Published Year",-18}{"Availability",-15}");
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");

                foreach (var book in books)
                {
                    if (book is ReferenceBook referenceBook)
                    {
                        referenceBook.DisplayBookDetails();
                    }
                    else if (book is LendingBook lendingBook)
                    {
                        lendingBook.DisplayBookDetails();
                    }
                }
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");

            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to fetch data to display..");
                LogError(e);
            }
        }

        public void SearchBook()
        {
            try
            {
                Book matchedBook = GetMatchingBook();
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
                Console.WriteLine($"\t{"Book ID",-10}{"Book Title",-15}{"Author",-15}{"Published Year",-18}{"Availability",-15}");
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
                if (matchedBook is ReferenceBook referenceBook)
                {
                    referenceBook.DisplayBookDetails();
                }
                else if(matchedBook is LendingBook lendingBook)
                {
                    lendingBook.DisplayBookDetails();
                }
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to fetch the details of  one book from the list...");
                LogError(e);
            }
        }

        public Book GetMatchingBook()
        {
            try
            {
                string bookIdOrTitle;
                do
                {
                    Console.Write("Enter a Book id or title : ");
                    bookIdOrTitle = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(bookIdOrTitle));

                Book matchedBook;
                if (int.TryParse(bookIdOrTitle, out int bookId))
                {
                    while (!books.Any(book => book.BookId == bookId))
                    {
                        Console.WriteLine($"No book available with this id - {bookId}");
                        bookId = GetBookId();
                    }
                    matchedBook = books.FirstOrDefault(books => books.BookId == bookId);
                }
                else
                {
                    while (!books.Any(book => book.BookTitle.ToLower() == bookIdOrTitle))
                    {
                        Console.WriteLine($"No book is available with this name {bookIdOrTitle}");
                        Console.Write("Enter a valid book name or enter Exit to stop : ");
                        bookIdOrTitle = Console.ReadLine().ToLower();
                        if (bookIdOrTitle == "exit") break;
                    }
                    matchedBook = books.FirstOrDefault(book => book.BookTitle.ToLower() == bookIdOrTitle);
                }
                if (matchedBook != null)
                {
                    return matchedBook;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to match the book id with the object");
                return null;
            }
        }

        //For select which operation 
        public int GetOption()
        {
            try
            {
                int option;
                Console.Write("Enter your choice(1-8) : ");
                while (!int.TryParse(Console.ReadLine(), out option) && option < 0 && option > 8)
                {
                    Console.Write("Enter a valid option : ");
                }
                return option;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                LogError(e);
                return 0;
            }
        }

        //For update book details
        public int GetChoice()
        {
            Console.Write("Enter your choice(1-4) : ");
            string choice = Console.ReadLine();
            int yourChoice;
            while (!int.TryParse(choice, out yourChoice) || yourChoice < 0 || yourChoice > 4)
            {
                Console.Write("Enter a valid choice(1-4) : ");
                choice = Console.ReadLine();
            }
            return yourChoice;
        }

        public int GetBookId()
        {
            try
            {
                Console.Write("Enter the Book id : ");
                string inputId = Console.ReadLine();
                int bookId;
                while (!int.TryParse(inputId, out bookId) || bookId <= 0)
                {
                    Console.Write("Enter a valid Book Id : ");
                    inputId = Console.ReadLine();
                }
                return bookId;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("unable to process the Given Book ID!");
                LogError(e);
                return 0;
            }
        }

        public string GetBookName()
        {
            try
            {
                Console.Write("Enter the Book name : ");
                string bookName = Console.ReadLine().ToLower();
                while (!(!int.TryParse(bookName, out _) && !string.IsNullOrWhiteSpace(bookName) && bookName.Length > 2 && bookName.All(c => Char.IsLetter(c) || c=='-' || c==' ' || c=='_')))
                {
                    Console.Write("Enter a valid book name : ");
                    bookName = Console.ReadLine().ToLower();
                }
                return bookName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Unable to process the Given Title of the book");
                LogError(e);
                return null;
            }
        }

        public string GetAuthorName()
        {
            try
            {
                Console.Write("Enter the Author name : ");
                string authorName = Console.ReadLine();
                while (!(!int.TryParse(authorName, out _) && !string.IsNullOrWhiteSpace(authorName) && authorName.Length > 2 && authorName.All(c => Char.IsLetter(c) || c == '_' || c == ' ')))
                {
                    Console.Write("Enter a valid Author name : ");
                    authorName = Console.ReadLine();
                }
                return authorName;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid author name...");
                LogError(e);
                return null;
            }
        }


        public int GetPublishedYear()
        {
            try
            {
                int year;
                Console.Write("Enter the year that book published : ");
                int todayYear = DateTime.Now.Year;
                while (!int.TryParse(Console.ReadLine(), out year) || year < 1800 || year > todayYear)
                {
                    Console.Write("Enter a valid year : ");
                }
                return year;
            }
            catch (Exception e)
            {
                Console.WriteLine("unable to process while get a year from the user..");
                LogError(e);
                return 0;
            }
        }

        public string GetBookType()
        {
            try
            {
                string bookType;
                do
                {
                    Console.Write("Enter \"L\" for lending book or \"R\" for reference book : ");
                    bookType = Console.ReadLine().Trim().ToLower();
                } while (bookType != "l" && bookType != "r");
                return bookType;
            }
            catch(Exception e)
            {
                Console.WriteLine("process is stopped while selecting book type...!");
                LogError(e);
                return null;
            }
        }

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
            catch(Exception e)
            {
                Console.WriteLine("Process is stopeed while get yes or no from the user to take decision....!");
                LogError(e);
                return null;
            }
        }

        public void LogError(Exception e)
        {
            string logDirecotryPath = "D:\\Azure-Assignment\\C#-Tasks\\C#-Tasks\\ConsoleApplication\\Library Book management system\\Error log files\\";
            using (StreamWriter writer = new StreamWriter(Path.Combine(logDirecotryPath, $"Error_log_{DateTime.Now.ToString("dd-MM-yyyy")}.txt"), true))
            {
                writer.WriteLine("\n==========================================================================================================================\n");
                writer.WriteLine($"Source : {e.Source} ");
                writer.WriteLine($"Error : {e.Message}");
                writer.WriteLine($"StackTrace : \n{e.StackTrace}");
                writer.WriteLine($"Date and Time : {DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}");
                writer.WriteLine("\n==========================================================================================================================");
            }
        }
    }
}
