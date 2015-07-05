using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferImage
{
    class Program
    {
        static void Main(string[] args)
        {
            //starte loglistener
            string logFilePath = @"C:\Users\Roger\Pictures\Bildanalyse\test.txt";
            LogListener logListener = new LogListener(logFilePath);
            logListener.LogReader();



        }
        
    }
}
