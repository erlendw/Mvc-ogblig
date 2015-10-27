using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nettButikkpls.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SaveDropzoneJsUploadedFiles()
        {
            

            foreach (string FileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[FileName];

                var _FileName = Path.GetFileName(file.FileName);

                var _Path = Path.Combine(Server.MapPath("~/App_Data/Images"), _FileName);

                file.SaveAs(_Path);


                Debug.Print(file.FileName);

            }

            return Json(new { Message = string.Empty });

        }

    }


}