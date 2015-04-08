using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppBildanalyse.Models;

namespace WebAppBildanalyse.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Image/
        public ActionResult Index()
        {
            var model = new Images()
            {
                Gallery = Directory.EnumerateFiles(Server.MapPath("~/Images"))
                          .Select(fn => "~/Images/" + Path.GetFileName(fn)).ToList()
            };

            return View(model);
        }
	}
}