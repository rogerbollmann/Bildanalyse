using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransferImage;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using UnitTestBildanalyse.ServiceReference1;
using TransferService;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace UnitTestBildanalyse
{
    [TestClass]
    public class UnitTestBildanalyse1
    {
        [TestMethod]
        public void TestLogListener()
        {
            //starte loglistener
            string logFilePath = @"C:\Users\Roger\Pictures\Bildanalyse\test.txt";
            string directoryPath = @"C:\Users\Roger\Pictures\Bildanalyse";
            string logFileName = "test.txt";
            LogListener logListener = new LogListener(logFilePath);
            


            
            //check logdirectory
            Assert.AreEqual(directoryPath, logListener.logDirectory);

            //check logfilepath
            Assert.AreEqual(logFilePath, logListener.logPath);

            //check logfileName
            Assert.AreEqual(logFileName, logListener.logFileName);

            using (StreamWriter fileWriter = File.AppendText(logFilePath))
            {
                string user = "Hans";
                string server = "svtest";
                string code = "200";
                string mime = "image/jpg";
                string app = "TestApp";
                string path = @"C:\Users\Roger\Documents\Visual Studio 2013\Projects\ConsoleApplication3\Log\IMG_20140723_113303.jpg";
                fileWriter.WriteLine("{0:HH:mm:ss}|{1}|{2}|{3}|{4}|{5}|{6}", DateTime.Now, path, mime, code, user, app, server);
                

            }

            object source = new Object();
            WatcherChangeTypes test = new WatcherChangeTypes();
            FileSystemEventArgs e = new FileSystemEventArgs(test, directoryPath, logFileName);
            logListener.OnChanged(source, e);
            
            
            //check if file is sended and translated to text
            Assert.AreEqual(File.Exists(@"C:\Users\Roger\Pictures\Output\svtestIMG_20140723_113303.txt"), true);

        }

        [TestMethod]
        public void TestTransferImageHandler()
        {
            string user = "Hans";
            string serverName = "svtest";
            string statusCode = "200";
            string mime = "image/jpg";
            string imageInformation = "User: " + user + ", Server: " + serverName + ", StatusCode: " + statusCode + ", Mime-Type: " + mime;
            string path = @"C:\Users\Roger\Documents\Visual Studio 2013\Projects\ConsoleApplication3\Log\IMG_20140723_113303.jpg";

            //string imagePath = @"C:\Users\Roger\Documents\Visual Studio 2013\Projects\ConsoleApplication3\Log\IMG_20140723_113303.jpg";
            string imageName = serverName + Path.GetFileName(path);

            TransferImageHandler trans = new TransferImageHandler(path, imageInformation, imageName);
            trans.SendImage();

            //check path
            Assert.AreEqual(path, trans.imagePath);

            //check imageInformation
            Assert.AreEqual(imageInformation, trans.imageInformation);

            //check imageName
            Assert.AreEqual(imageName, trans.fileName);

            //check if file is sended and translated to text
            Assert.AreEqual(File.Exists(@"C:\Users\Roger\Pictures\Output\svtestIMG_20140723_113303.txt"), true);
        }
        [TestMethod]
        public void TestTransferImageToService()
        {

            
            //datetime|path|mime|code|user|app|servername
            
            string user = "Hans";
            string serverName = "svtest";
            string statusCode = "200";
            string mime = "image/jpg";

            string imagePath = @"C:\Users\Roger\Documents\Visual Studio 2013\Projects\ConsoleApplication3\Log\IMG_20140723_113303.jpg";


            string imageInformation = "User: " + user + ", Server: " + serverName + ", StatusCode: " + statusCode + ", Mime-Type: " + mime;

            string fileName = "IMG_20140723_113303.jpg";

            //create a Service client and send the image to Webservice
            Service1Client client = new Service1Client("BasicHttpBinding_IService1");
            Byte[] fileByte = File.ReadAllBytes(imagePath);
            client.UploadImage(fileName, imageInformation, fileByte);

            Assert.AreEqual(File.Exists(@"C:\Users\Roger\Pictures\Output\IMG_20140723_113303.txt"), true);
            /**
            string lastLine = "";

            using (StreamReader sr = new StreamReader(@"C:\Users\Roger\Pictures\Output\IMG_20140723_113303.txt"))
            {
                
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (sr.Peek() == -1)
                    {
                        lastLine = line;
                    }
                }
            }
            **/
            


            var lines = File.ReadLines(@"C:\Users\Roger\Pictures\Output\IMG_20140723_113303.txt");
            string lastLine = lines.Last();

            Assert.AreEqual(imageInformation, lastLine);



            
        }
        [TestMethod]
        public void TestTranslator()
        {
            string fileName="IMG_20140723_113303.jpg";
            string source = @"C:\Users\Roger\Documents\Visual Studio 2013\Projects\ConsoleApplication3\Log";
            string dest = @"C:\Users\Roger\Pictures\Input";

            string sourceFile = Path.Combine(source, fileName);
            string destFile = Path.Combine(dest, fileName);

            File.Copy(sourceFile, destFile, true);
            Translator trans = new Translator(fileName);
            string pathOutputFile = trans.transLate();

            string expectedOutputPath = "C:\\Users\\Roger\\Pictures\\Output\\IMG_20140723_113303";

            Assert.AreEqual(expectedOutputPath, pathOutputFile);

        }
    }
}
