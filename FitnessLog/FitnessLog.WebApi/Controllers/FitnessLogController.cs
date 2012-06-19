using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessLog.WebApi.Controllers
{
    public class FitnessLogController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Log()
        {
            return View();
        }

        public ActionResult EditLogEntry()
        {
            return View();
        }
    }
}
