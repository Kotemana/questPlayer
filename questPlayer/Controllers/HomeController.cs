using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using questPlayer.App_Start;
using questPlayer.Models;

namespace questPlayer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            FileList.FillQuests();
            var model = FileList.Quests;

            return View(model);
        }
    }
}