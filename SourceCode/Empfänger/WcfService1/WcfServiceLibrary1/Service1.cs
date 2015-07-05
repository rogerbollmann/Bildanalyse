using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public double Add(double x, double y)
        {
            return x + y;
        }

        public void UploadFile(byte[] img, string imageName, string imageInformation, long imageSize)
        {
            FileStream targetStream = null;
            MemoryStream sourceStream = new MemoryStream(img);
            

            string uploadFolder = @"C:\Users\Roger\Pictures\";
            string filename = imageName;
            string filePath = Path.Combine(uploadFolder, filename);

            using (targetStream = new FileStream(filePath, FileMode.Create,
                                      FileAccess.Write, FileShare.None))
            {
                //read from the input stream in 4K chunks
                //and save to output stream
                targetStream.CopyTo(sourceStream);
                /*const int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }*/
                Console.WriteLine(imageSize);
                Console.WriteLine(imageInformation);
                targetStream.Close();
                //sourceStream.Close();
            }
        }
    }
}
