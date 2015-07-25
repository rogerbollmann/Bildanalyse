using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyIIS7Project
{
    class MyHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false;  }
        }

        public void ProcessRequest(HttpContext context)
        {
            string contentType = context.Response.ContentType;

            if (contentType.Contains("image"))
            {
                using (StreamWriter stream = File.AppendText(@"C:\Users\Roger\Documents\GitHub\Bildanalyse\SourceCode\Sender\MyIIS7Project\Log\test_handler.txt"))
                {
                    //log all information
                    stream.WriteLine(context.Response.Headers);
                    stream.WriteLine(context.Response.ContentType);
                    stream.WriteLine(context.Request.PhysicalPath);
                    stream.WriteLine(context.Request.LogonUserIdentity);
                    stream.WriteLine(context.Request.PhysicalApplicationPath);
                }

            }
        }
    }
}
