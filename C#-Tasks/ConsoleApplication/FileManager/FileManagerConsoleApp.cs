using System.Configuration;
using System.Reflection;
using System.Text;
using Tasks.ConsoleApplication;

//FileManagerConsoleApp fileManagerConsoleApplicaiton = new FileManagerConsoleApp();
//fileManagerConsoleApplicaiton.CRUD();

namespace Tasks.ConsoleApplication
{
    internal class FileManagerConsoleApp
    {
        string directoryPath = ConfigurationManager.AppSettings["directoryPath"];

        public void CRUD()
        {
            Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("*****************************************    FILE MANAGER   **********************************************");
            Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");

            Directory.SetCurrentDirectory(directoryPath);
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Console.WriteLine($"Do you want to work on this folder  :  {directoryPath} : ");
                    bool proceed = GetYesOrNo();
                    if (proceed)
                    {
                        Directory.SetCurrentDirectory(directoryPath);
                        CrudOperation();
                    }
                    else
                    {
                        Console.Write("Enter a new path to the directory (format example :D:\\Folder\\Practice\\) or Directory name : ");
                        string directoryPathOrName = Console.ReadLine();
                        if (File.Exists(directoryPathOrName))
                        {
                            Directory.SetCurrentDirectory(directoryPathOrName);
                        }
                        else
                        {
                            Directory.CreateDirectory(directoryPathOrName);
                            Directory.SetCurrentDirectory(directoryPathOrName);
                            Console.WriteLine("Your directory is created successfully!.........");
                        }
                        CrudOperation();
                    }
                }
                else
                {
                    Console.WriteLine("Error  : The folder doesn't exist ");
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("The directory doesn't exist. could you check your folder once? ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Something wrong in the folder ");
            }
        }


        public void CrudOperation()
        {
            //string directoryPath = @"D:\Practice Folder\";
            int choice;
            do
            {
                Console.WriteLine("\n--- File Manager ---\r\n1. Create File\r\n2. Read File\r\n3. Append to File\r\n4. Delete File\r\n5. Exit");
                choice = GetChoice();
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                switch (choice)
                {
                    case 1:
                        ShowExistingFiles();
                        Console.WriteLine("Creating a file : ");
                        string fileName = GetFileName();
                        try
                        {
                            if (File.Exists(fileName))
                            {
                                Console.WriteLine($"The file is already exist in {fileName}");
                                Console.WriteLine("Do you want to rename this file ? ");
                                bool RefactorName = GetYesOrNo();
                                if (RefactorName)
                                {
                                    string newFileName = GetFileName();
                                    File.Move(fileName, newFileName);
                                }
                            }
                            else
                            {
                                Console.WriteLine("The file doesn't exist...");
                                Console.WriteLine("Do you want to create a exmpty file in this folder?");
                                bool isTrue = GetYesOrNo();
                                if (isTrue)
                                {
                                    File.CreateText(fileName).Close();
                                    Console.WriteLine("The Empty file is creaded successfully");
                                }
                                else
                                {
                                    Console.WriteLine("nothing happened in this folder ");
                                }
                            }
                        }
                        catch (DirectoryNotFoundException e)
                        {
                            Console.WriteLine("The directory is not found ");
                        }
                        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");

                        break;

                    case 2:
                        ShowExistingFiles();

                        Console.WriteLine("Read a file : ");
                        try
                        {
                            fileName = GetFileName();
                            //pathWithFileName = Path.Combine(directoryPath, fileName);
                            if (File.Exists(fileName))
                            {
                                if (File.ReadAllText(fileName).Length == 0)
                                {
                                    Console.WriteLine("The file is empty!.....");
                                }
                                else
                                {

                                    using (StreamReader reader = new StreamReader(fileName))
                                    {
                                        string line;
                                        while ((line = reader.ReadLine()) != null)
                                        {
                                            Console.WriteLine(line);
                                        }
                                        Console.WriteLine(line);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("File not found !.......");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("The file not found......");
                        }
                        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                        break;

                    case 3:
                        ShowExistingFiles();
                        Console.WriteLine("Append  a file : ");
                        try
                        {
                            fileName = GetFileName();
                            using (StreamWriter writer = new StreamWriter(fileName))
                            {
                                string userGivenContent;
                                Console.WriteLine("Enter the content that you want to add in your file or press \"Enter\" twice to exit ");
                                do
                                {
                                    userGivenContent = Console.ReadLine();
                                    writer.WriteLine(userGivenContent);
                                } while (!string.IsNullOrWhiteSpace(userGivenContent));
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Sorry i think the file doesn't exist");
                        }
                        Console.WriteLine("All your contents are written in your file");
                        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                        break;

                    case 4:
                        ShowExistingFiles();
                        try
                        {
                            Console.WriteLine("Delete  a file : ");
                            Console.WriteLine("Enter the filename that you want to delete ");
                            fileName = GetFileName();
                            //pathWithFileName = Path.Combine(directoryPath, fileName);
                            if (File.Exists(fileName))
                            {
                                Console.WriteLine($"Do you really want to delete this file({fileName}) ");
                                bool ConfirmationForDelete = GetYesOrNo();
                                if (ConfirmationForDelete)
                                {
                                    File.Delete(fileName);
                                    Console.WriteLine("your file is deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("The file is not get deleted as you said..");
                                    Console.WriteLine("Do you want to empty this file");
                                    bool ConfirmationForEmpty = GetYesOrNo();
                                    if (ConfirmationForEmpty)
                                    {
                                        File.WriteAllText(fileName, "");
                                    }
                                    else
                                    {
                                        Console.WriteLine("The file content is not deleted from the file");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("The file doesn't exist in the current repository.");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Sorry i think the file doesn't exist");
                        }
                        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
                        break;

                    case 5:
                        Console.WriteLine("Exititing............ ");
                        break;

                    default:
                        Console.WriteLine("Invalid choice....");
                        break;
                }

            } while (choice != 5);
        }
        public string GetFileName()
        {
            string fileName;
            Console.Write("Enter a file name with extension(.txt) : ");
            fileName = Console.ReadLine();
            try
            {
                while (string.IsNullOrEmpty(fileName) || !IsCorrectFileName(fileName) || fileName.Length < 2)
                {
                    Console.Write("Enter a valid file name : ");
                    fileName = Console.ReadLine();
                }
                if (!fileName.EndsWith(".txt")) fileName += ".txt";
                return fileName;
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine("The file name should not contain any unwanted symbols...");
                    Console.WriteLine(ex.Message);
                }
            }
            return fileName;
        }

        bool IsCorrectFileName(string fileName)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in fileName)
            {
                if (invalidChars.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

        public bool GetYesOrNo()
        {
            Console.Write("Enter yes or no (y/n) : ");
            string yes = Console.ReadLine().ToLower();
            if (yes.ToLower() == "y")
                return true;
            else if (yes.ToLower() == "n") return false;
            else
            {
                while (!(yes.ToLower() == "n" || yes.ToLower() == "y"))
                {
                    Console.Write("Enter a valid value (y/n)  : ");
                    yes = Console.ReadLine();
                }
                return yes == "y" ? true : false;
            }
        }

        public int GetChoice()
        {
            Console.Write("Enter your choice : ");
            string choice = Console.ReadLine();
            int selectedChoice;
            while (!(int.TryParse(choice, out selectedChoice) && selectedChoice >= 1 && selectedChoice <= 5))
            {
                Console.Write("Enter a valid choice (1-5) : ");
                choice = Console.ReadLine();
            }
            return selectedChoice;
        }

        public void ShowExistingFiles()
        {
            string[] filepaths = Directory.GetFiles(directoryPath);
            Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Existing files : ");
            foreach (string filepath in filepaths)
            {
                Console.WriteLine("\t" + Path.GetFileName(filepath));
            }
            Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------\n");

        }
    }
}
