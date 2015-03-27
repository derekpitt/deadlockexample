using Foundatio.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DeadlockExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Deadlock()
        {
            var fs = new FolderFileStorage(System.IO.Path.GetTempPath());

            fs.SaveObject("whammy", "ok");
            return View();
        }


        public async Task<ActionResult> NoDeadlock()
        {
            var fs = new FolderFileStorage(System.IO.Path.GetTempPath());

            await fs.SaveObjectAsync("whammy", "ok");
            return View();
        }
    }
}
