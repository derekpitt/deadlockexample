using Foundatio.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DeadlockExample.Controllers
{
    internal static class FileStorageExts
    {
        public static bool SaveObjectConfigAwaitFalse<T>(this IFileStorage storage, string path, T data)
        {
            return Task.Run(() => storage.SaveObjectAsyncConfigAwaitFalseAsync(path, data)).Result;
        }

        public static async Task<bool> SaveObjectAsyncConfigAwaitFalseAsync<T>(this IFileStorage storage, string path, T data, CancellationToken cancellationToken = default(CancellationToken))
        {
            string json = JsonConvert.SerializeObject(data);
            return await storage.SaveFileAsync(path, new MemoryStream(Encoding.UTF8.GetBytes(json)), cancellationToken).ConfigureAwait(false);
        }

    }

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

        public ActionResult NoDeadlockConfigAwait()
        {
            var fs = new FolderFileStorage(System.IO.Path.GetTempPath());

            // damn..  still deadlocks with config await false
            fs.SaveObjectConfigAwaitFalse("whammy", "ok");

            // testing to see if using configwait false removes context??
            System.Console.WriteLine(Request.UserAgent);
            return View();
        }
    }
}
