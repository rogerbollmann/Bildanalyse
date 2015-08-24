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
        public string uploadFolder;

        public void UploadImage(string fileName, string fileInfo, byte[] data)
        {
            this.uploadFolder = ConfigurationSettings.AppSettings["input"];
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
                string fileOutPath = trans.transLate() + ".txt";

                using (StreamWriter writer = File.AppendText(fileOutPath))
                {
                    writer.WriteLine(Environment.NewLine+fileInfo);

                }
            }
            catch (Exception ex)
            {
                //...
                Console.WriteLine(ex.Message);
                //return "Something went wrong: " + ex.Message;
            }

        }
    }
}
