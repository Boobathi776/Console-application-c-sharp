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
        public int MobileNumber { get; set; }
        public string Gmail { get; set; }

        public User(int userId,string userName,int mobileNumber,string gmail)
        {
            UserID = userId;
            UserName = userName;
            //Password = password;    
            MobileNumber = mobileNumber;
            Gmail = gmail;
        }
    }
}
