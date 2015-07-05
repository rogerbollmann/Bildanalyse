using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TransferService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {

        public void UploadImage(string fileName, string fileInfo, byte[] data)
        {
            string uploadFolder = ConfigurationSettings.AppSettings["input"];
            string filePath = Path.Combine(uploadFolder, fileName);

            BinaryWriter Writer = null;
            //string Name = @"C:\temp\yourfile.name";

            try
            {
                // Create a new stream to write to the file
                Writer = new BinaryWriter(File.OpenWrite(filePath));

                // Writer raw data                
                Writer.Write(data);
                Writer.Flush();
                Writer.Close();

                //start translation to text
                Translator trans = new Translator(fileName);
                trans.transLate();
                
            }
            catch (Exception ex)
            {
                //...
                Console.WriteLine(ex.Message);
                //return "Something went wrong: " + ex.Message;
            }

        }
        /*
        public void UploadFile(FileStream uploadFile)
        {
            string uploadFolder = @"C:\Users\Roger\Pictures\";
            string fileName = "Image.jpg";

            string filePath = Path.Combine(uploadFolder, fileName);

            using (FileStream targetStream = new FileStream(filePath, FileMode.Create,
                                      FileAccess.Write, FileShare.None))
            {
                //read from the input stream in 4K chunks
                //and save to output stream
                targetStream.CopyTo(uploadFile);
                /*const int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }
                //Console.WriteLine(imageSize);
                //Console.WriteLine(imageInformation);
                targetStream.Close();
                //sourceStream.Close();

            }
        }*/
    }
}
