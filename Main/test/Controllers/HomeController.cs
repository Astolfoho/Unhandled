using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unhandled.Api;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            Response.Cookies.Add(new HttpCookie("bla", "vaiPlaneta"));

            return RedirectToAction("Not");
        }

        public string Not()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new Exception("tem uma inner aqui",ex);
            }
          
        }

        public int Divide()
        {
            int a = 1;
            int b = 0;

            return a / b;
        }

        public string ScriptError()
        {
            return "<script type=\"text/javascript\"> "
                      + "window.onerror = function(ev) {"
                      + "alert(\"Error caught:\" + ev.toString());"
                      + "};"
                      + "xxx();function xxx(){ throw 'test';}"
                      + "</script>";
        }
    }
}