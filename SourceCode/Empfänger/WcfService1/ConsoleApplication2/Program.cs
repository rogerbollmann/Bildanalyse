using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication2.Bildanalyse;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client client = new Service1Client("BasicHttpBinding_IService1");
            //Service1Client client = new Service1Client("NetTcpBinding_IService11");
            
            //FileUploadMessage file = new FileUploadMessage();

            string user = "Roger Bollmann";
            string serverName = "svtest";
            string statusCode = "200";

            string imageInformation = "User: " + user + ", Server: " + serverName + ", StatusCode: " + statusCode;

            string fileName = @"C:\Users\Roger\Documents\Visual Studio 2013\Projects\ConsoleApplication3\Log\IMG_20140723_113303.jpg";
            string imageName = "IMG_20140723_113303.jpg";
            byte[] fileByte = File.ReadAllBytes(fileName);
            FileInfo info = new FileInfo(fileName);
            long imageSize = info.Length;
            client.UploadFile(fileByte, imageName, imageInformation, imageSize);



        }
    }
}
