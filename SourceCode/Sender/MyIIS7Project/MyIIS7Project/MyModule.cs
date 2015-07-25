using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace MyIIS7Project
{
    public class MyModule : IHttpModule
    {
        public MyModule()
        {

        }

        public String ModulName
        {
            get { return "MyModul"; }
        }

        public void Dispose()
        {
            //managed by CC
        }

        public void Init(HttpApplication context)
        {
            //Register to the EndRequest event
            //context.EndRequest += new EventHandler(this.PictureAnalysis);
            //context.BeginRequest += new EventHandler(this.begin_RequestHandler);

        }

        private void begin_RequestHandler(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if (app != null)
            {
                string path = @"C:\Users\Roger\Documents\GitHub\Bildanalyse\SourceCode\Sender\MyIIS7Project\Log\test.txt";

                using (FileStream fs = File.Open(path, FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine("{0:HH:mm:ss} - {1}", DateTime.Now, app.Request.PhysicalPath);
                    }
                }
            }
        }

        private void PictureAnalysis(object httpContent, EventArgs eventArgs)
        {
            HttpApplication objApp = (HttpApplication) httpContent;
            HttpResponse objResponse = (HttpResponse) objApp.Response;
            HttpRequest objRequest = (HttpRequest) objApp.Request;
            using (StreamWriter stream = new StreamWriter(@"C:\Users\Roger\Documents\GitHub\Bildanalyse\SourceCode\Sender\MyIIS7Project\Log\test.txt"))
            {
                //log all information
                stream.WriteLine(objApp.Response.Headers);
                stream.WriteLine(objApp.Context.Response.ContentType);
                stream.WriteLine(objApp.Context.Request.PhysicalPath);
                stream.WriteLine(objApp.Context.Request.LogonUserIdentity);
                stream.WriteLine(objApp.Context.Request.PhysicalApplicationPath);
            }
            /**
            String mimetype = objResponse.ContentType;
            int statusCode = objResponse.StatusCode;
            Regex regex = new Regex(@"2[0-9][0-9]");
            Match match = regex.Match(statusCode.ToString());
            if (mimetype.Contains("image") &&  match.Success)
            {
                //Get values
                String pathtoImage = objRequest.PhysicalPath;
                String userName = objRequest["userid"].ToString();
                String serverName = objRequest["Host"].ToString();
                String appName = objRequest.PhysicalApplicationPath;

                string logEntry=String.Format(pathtoImage+"|"+mimetype+"|"+statusCode.ToString()+"|"+userName+"|"+serverName+"|"+appName);

                
                using (StreamWriter stream = new StreamWriter(@"C:\Users\Roger\Documents\GitHub\Bildanalyse\SourceCode\Sender\MyIIS7Project\Log\test.txt"))
                {
                    //log all information
                    stream.WriteLine(logEntry);
                }

                
            }**/
        }
    }
}
