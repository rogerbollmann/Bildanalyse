﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferImage
{
    public class LogListener
    {
        public string logPath;
        public string logDirectory;
        public string logFileName;
        public LogListener(string path){

            this.logPath = path;
            this.logDirectory = Path.GetDirectoryName(path);
            this.logFileName = Path.GetFileName(path);

        }

        /**
        public string LogPath{ get; private set;}
        public string LogDirectory { get; private set; }
        public string LogFileName { get; private set; }
        **/

        public void LogReader()
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            //watcher.Path = @"C:\Users\Roger\Pictures\Bildanalyse\";
            watcher.Path = logDirectory;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.FileName;
            // Only watch text files.
            watcher.Filter = logFileName;

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);
          

            // Add event handlers.
            

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            //Console.WriteLine("Press \'q\' to quit the sample.");
            while (true) ;
        }

        
        // Define the event handlers. 
        public void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            
            using (FileStream fileStream = File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {

                var lines = File.ReadLines(e.FullPath);
                string line = lines.Last();

                //datetime|path|mime|code|user|app|servername
                string[] info = line.Split(new Char [] {'|'});

                string path = info[1];
                string mime = info[2];
                string statusCode = info[3];
                string user = info[4];
                string app = info[5];
                string serverName = info[6];

                string imageInformation = "User: " + user + ", Server: " + serverName + ", StatusCode: " + statusCode + ", Mime-Type: "+mime;

                //string imagePath = @"C:\Users\Roger\Documents\Visual Studio 2013\Projects\ConsoleApplication3\Log\IMG_20140723_113303.jpg";
                string imageName = serverName + Path.GetFileName(path);

                TransferImageHandler trans = new TransferImageHandler(path, imageInformation, imageName);
                trans.SendImage();
                //SendImage(imagePath, imageInformation, imageName);

            }
            
        }
        /**
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }**/
    }
}
