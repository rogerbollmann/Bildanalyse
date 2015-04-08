using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        
        
        
        static void Main(string[] args)
        {
            Run();
            

        }

        private static void Run()
        {
            string logFile = "test.txt";
            string pathToImageFolder = @"C:\Users\Roger\Documents\Visual Studio 2013\Projects\ConsoleApplication3\Log\Images\";
            string pathToLogFolder = "C:\\Users\\Roger\\Documents\\Visual Studio 2013\\Projects\\ConsoleApplication3\\Log";
            LogListener log = new LogListener(logFile, pathToImageFolder, pathToLogFolder);
            //log.LogListenerStart();
            log.LogReader();
        }
            



            
    }
}
