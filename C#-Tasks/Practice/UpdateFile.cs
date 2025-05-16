using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Practice;

internal class UpdateFile
{
    public void Update()
    {
        string directoryPath = ConfigurationManager.AppSettings["practicePath"];
        string fileName = "updated File.txt";
        string fullPath = Path.Combine(directoryPath, fileName);
        string[] allLines = File.ReadAllLines(fullPath);
        using (StreamWriter writer = new StreamWriter(fullPath))
        {
            
        }
    }
}
