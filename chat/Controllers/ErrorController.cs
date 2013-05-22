using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chat.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            ViewData["ErrorType"] = "Server error";
            ViewData["ErrorMessage"] = "Error 500";
            ViewData["Title"] = "Error 500";
            return View();
        }

        public ActionResult Error404()
        {
            ViewData["ErrorType"] = "Page does not exist";
            ViewData["ErrorMessage"] = "Error 404";
            ViewData["Title"] = "Error 404";
            return View("Index");
        }

    }
}
