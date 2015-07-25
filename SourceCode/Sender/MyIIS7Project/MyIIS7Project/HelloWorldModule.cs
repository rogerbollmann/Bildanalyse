using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace MyIIS7Project
{
    public class HelloWorldModule : IHttpModule
    {
        public HelloWorldModule()
        {
        }

        public String ModuleName
        {
            get { return "HelloWorldModule"; }
        }

        // In the Init function, register for HttpApplication 
        // events by adding your handlers.
        public void Init(HttpApplication application)
        {
            application.BeginRequest +=
                (new EventHandler(this.Application_BeginRequest));
            application.EndRequest +=
                (new EventHandler(this.Application_EndRequest));
        }

        private void Application_BeginRequest(Object source,
             EventArgs e)
        {
            // Create HttpApplication and HttpContext objects to access
            // request and response properties.
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            string filePath = context.Request.FilePath;
            string mimetype = context.Request.ContentType;


            
            if (mimetype.Contains("image"))
            {
                string path = @"C:\inetpub\logs\LogFiles\test_begin.txt";

                using (FileStream fs = File.Open(path, FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine("{0:HH:mm:ss} - {1}|{2}", DateTime.Now, context.Request.PhysicalPath, mimetype);
                    }
                }
            }
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            string contentType = context.Response.ContentType;
            string statusCode = context.Response.StatusCode.ToString();
            //string serverName = context.Request["Host"].ToString();
            string serverName = context.Request.UserHostName;
            string userName;
            try
            {
                userName = context.Request["userid"].ToString();
            }
            catch (Exception)
            {

                userName = "anonymous";
            }

            string applicationName;

            if (String.IsNullOrEmpty(context.Request.ApplicationPath))
            {
                applicationName = context.Request.ApplicationPath;
            }
            else
            {
                applicationName = "Default-Application";
            }
            
            string logfile;
            try
            {
                logfile = ConfigurationSettings.AppSettings["logFilePath"];
            }
            catch (Exception)
            {
                
                logfile = @"C:\inetpub\logs\LogFiles\test_nopath.txt";
            }

            if (contentType.Contains("image"))
            {
                using (StreamWriter stream = File.AppendText(logfile))
                {

                    stream.WriteLine("{0:HH:mm:ss}|{1}|{2}|{3}|{4}|{5}|{6}", DateTime.Now, context.Request.PhysicalPath, contentType, statusCode,userName, serverName, applicationName);

                }

            }

            contentType = "";
            
            
        }

        public void Dispose() { }
    }
}
