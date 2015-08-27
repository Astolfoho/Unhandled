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
        public string Index()
        {
            try
            {
                throw new Exception("aff");
            }
            catch (Exception ex)
            {
                UnhandledApi.Instance.WriteException(ex);
            }

            throw new Exception("Vamos ver se funciona");
            return "<h1>Vai Planeta</h1>";
        }

        public string Not()
        {
            throw new NotImplementedException();
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