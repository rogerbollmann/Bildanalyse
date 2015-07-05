using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferImage
{
    class TransferImageHandler
    {
        string imagePath;
        string imageInformation;
        string fileName;
        public TransferImageHandler(string imagePath, string imageInformation, string fileName)
        {
            this.imageInformation = imageInformation;
            this.imagePath = imagePath;
            this.fileName = fileName;
        }
        public void SendImage()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");
            
            


            byte[] fileByte = File.ReadAllBytes(imagePath);
            client.UploadImage(fileName, imageInformation, fileByte);

        }

        public void Echo()
        {
            Console.WriteLine("Image Path: " + imagePath);
            Console.WriteLine("Image Information: " + imageInformation);
            Console.WriteLine("File name: " + fileName);
        }
    }
}
