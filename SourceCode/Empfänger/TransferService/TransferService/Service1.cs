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
        public string logfileService;

        public void UploadImage(string fileName, string fileInfo, byte[] data)
        {
            this.uploadFolder = ConfigurationSettings.AppSettings["input"];
            this.logfileService = ConfigurationSettings.AppSettings["logfileService"];
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

                //write image information into the output file of the Translator
                using (StreamWriter writer = File.AppendText(fileOutPath))
                {
                    writer.WriteLine(Environment.NewLine+fileInfo);

                }
            }
            catch (Exception ex)
            {
                //in case of errors, write the error message into the logfile
                using (StreamWriter writer = File.AppendText(logfileService))
                {
                    writer.WriteLine(ex.Message);

                }
            }

        }
    }
}
