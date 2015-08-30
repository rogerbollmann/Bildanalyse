using System;
using System.Collections.Generic;
using System.Configuration;
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
            //string logFilePath = @"C:\Users\Roger\Pictures\Bildanalyse\test.txt";
            string logFilePath = ConfigurationSettings.AppSettings["logFile"];
            string logFileTransHandler = ConfigurationSettings.AppSettings["logFileTransHandler"];

            var failureLine = "";

            //read all lines in from the logfile which was created by TransferHandler in case of Errors.
            if (File.Exists(logFileTransHandler))
            {

                var lines = File.ReadAllLines(logFileTransHandler);
                File.Delete(logFileTransHandler);
                foreach (var item in lines)
                {
                    string[] info = item.Split(new Char[] { '|' });
                    string imagePath = info[0];
                    string imageInformation = info[1];
                    string fileName = info[2];

                    TransferImageHandler tim = new TransferImageHandler(imagePath, imageInformation, fileName);
                    tim.SendImage();
                }
            }
            

            //start loglistener
            LogListener logListener = new LogListener(logFilePath);
            logListener.LogReader();



        }
        
    }
}
