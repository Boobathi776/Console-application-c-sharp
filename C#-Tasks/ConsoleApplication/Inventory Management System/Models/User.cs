using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.ConsoleApplication.Inventory_Management_System.Models
{
    internal class User
    {
        public int UserID { get; init; }
        public string UserName { get; init; }
        //public string Password { get; init; }
        public string MobileNumber { get; set; }
        public string Gmail { get; set; }
        public string TextFile { get; set; }
        public User(int userId,string userName,string mobileNumber,string gmail,string textFile="empty")
        {
            UserID = userId;
            UserName = userName;
            //Password = password;    
            MobileNumber = mobileNumber;
            Gmail = gmail;
            TextFile = $"{userName}.txt";
        }
    }
}
