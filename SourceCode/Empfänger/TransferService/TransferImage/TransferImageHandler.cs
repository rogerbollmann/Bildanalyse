using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferImage
{
    public class TransferImageHandler
    {
        public string imagePath;
        public string imageInformation;
        public string fileName;
        string logFilePathTransHand;
        public TransferImageHandler(string imagePath, string imageInformation, string fileName)
        {
            this.imageInformation = imageInformation;
            this.imagePath = imagePath;
            this.fileName = fileName;
            this.logFilePathTransHand = ConfigurationSettings.AppSettings["logFileTransHandler"];
        }
        public void SendImage()
        {
            try
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");
                byte[] fileByte = File.ReadAllBytes(imagePath);
                client.UploadImage(fileName, imageInformation, fileByte);
            }
            catch (Exception ex)
            {

                using (StreamWriter writer = File.AppendText(logFilePathTransHand))
                {
                    writer.WriteLine("{0}|{1}|{2}|{3}",imagePath,imageInformation,fileName,ex.Message);

                }
            }

        }

        public void Echo()
        {
            Console.WriteLine("Image Path: " + imagePath);
            Console.WriteLine("Image Information: " + imageInformation);
            Console.WriteLine("File name: " + fileName);
        }
    }
}
