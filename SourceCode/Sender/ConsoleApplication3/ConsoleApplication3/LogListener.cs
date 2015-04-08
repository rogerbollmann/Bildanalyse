using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class LogListener
    {
        public string logFile;
        public string pathToImageFolder;
        public string pathToLogFolder;
        public LogListener(string logFile, string imageFolder, string logFolder)
        {
            this.pathToImageFolder = imageFolder;
            this.pathToLogFolder = logFolder;
            this.logFile = logFile;

        }
        public void LogReader()
        {
            string log = pathToLogFolder + "\\" + logFile;
            using (var fs = new FileStream(log, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            using (var reader = new StreamReader(fs))
            {
                while (true)
                {
                    var line = reader.ReadLine();

                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        //Console.WriteLine("Line read: " + line);
                        //return path to Image
                        string image = SplitLogFile(line, 0);
                        CopyImage(image, pathToImageFolder);


                    }
                        

                }
            }
        }

        //Split Line into a list and return the value which is needed.
        public string SplitLogFile(string logLine, int index)
        {
            List<string> pathToImageList = logFile.Split(' ').ToList();
            //0. Path to Image, 1. Mime Type, 2. Status Code, 3. Username, 4. AppName, 5. Servername
            return pathToImageList[index];

        }

        //Copy Image to the folder
        public void CopyImage (string sourceImage, string destFolder)
        {
            string fileName = Path.GetFileName(sourceImage);
            string destImage = destFolder + "\\" + fileName;
            File.Copy(sourceImage, destImage, true);
        }

        
        public void LogListenerStart()
        {
            String dir = pathToLogFolder;
            List<string> args = new List<string>();
            args.Add(dir);


            // If a directory is not specified, exit program.
            if (args.Count < 1)
            {
                // Display the proper way to call the program. Usage: Watcher.exe (directory)
                Console.WriteLine("No directory is specified");
                return;
            }

            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = args[0];
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Only watch text files.
            watcher.Filter = logFile;

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            String line;
            String path = "C:\\Users\\Roger\\Documents\\Visual Studio 2013\\Projects\\ConsoleApplication3\\Log\\test.txt";
            String path2 = "C:\\Users\\Roger\\Documents\\Visual Studio 2013\\Projects\\ConsoleApplication3\\Log\\test2.txt";
            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("image"))
                    {
                        lines.Add(line);
                        
                    }

                    
                    
                } ;
                using (StreamWriter sw = new StreamWriter(path2))
                {
                    foreach (string image in lines)
                    {
                        sw.WriteLine(image);
                    }


                }

                for (int i = 0; i < lines.Count; i++)
                {
                    
                    Console.WriteLine("image found: {0}", lines[i]);
                }

            }

            using (StreamReader srFile = new StreamReader(path2))
            {
                while ((line = srFile.ReadLine()) != null)
                {
                    
                    string fileName;
                    string imagePath;
                    List<string> sourcePathFiles = new List<string>();
                    sourcePathFiles = line.Split('"').ToList();
                    

                    foreach (var sourcePath in sourcePathFiles)
                    {
                        if (sourcePath.Contains("jpg") || sourcePath.Contains("png"))
                        {

                            fileName = Path.GetFileName(sourcePath);
                            imagePath = @"C:\Users\Roger\Documents\Visual Studio 2013\Projects\ConsoleApplication3\Log\Images\"+fileName;
                            File.Copy(sourcePath, imagePath, true);
                            fileName = "";
                            imagePath = "";

                        }
   
                    }
                    



                };

            }

            

        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        
        }

    }
}
