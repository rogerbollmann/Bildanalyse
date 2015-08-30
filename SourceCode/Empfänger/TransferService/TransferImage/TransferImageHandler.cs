using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferImage
{
    /**
     * TransferImageHandler is responsible for transfer the image to the webservice
     **/
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
            //read from config file
            this.logFilePathTransHand = ConfigurationSettings.AppSettings["logFileTransHandler"];
        }

        /**
         * Send image to webservice
         **/

        public void SendImage()
        {
            try
            {
                //Create a Service client for using the public methode of the service
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");
                
                //read the file in as binary
                byte[] fileByte = File.ReadAllBytes(imagePath);

                //upload the image to the webservice
                client.UploadImage(fileName, imageInformation, fileByte);
            }
            catch (Exception ex)
            {
                //in case of errors, write this error message into a logfile
                using (StreamWriter writer = File.AppendText(logFilePathTransHand))
                {
                    writer.WriteLine("{0}|{1}|{2}|{3}",imagePath,imageInformation,fileName,ex.Message);

                }
            }

        }

    }
}
