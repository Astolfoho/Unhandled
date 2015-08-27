using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using Unhandled.Base;
using Unhandled.Factories.Repository;
using Unhandled.Models;
using Unhandled.Repository.Interfaces;

namespace Unhandled.HttpModules
{
    public class UnhundledViewer : BaseModule
    {

        private const string INDEX_HANDLER_CONST = "unhandled.axd";
        private readonly Regex _fileNameRegex
            = new Regex(@"(.*\/(?<fileName>.*)\.(?<extension>.*))?[\.|\/]unhandled\.axd", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        public override void Dispose()
        {
        }

        public override void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            if (_fileNameRegex.IsMatch(Request.RawUrl.ToLower()))
            {
                var match = _fileNameRegex.Match(Request.RawUrl.ToLower());
                var extension = match.Groups["extension"].ToString();
                var filename = match.Groups["fileName"].ToString();

                if (!string.IsNullOrEmpty(filename))
                {
                    Response.WriteEmbededFile(filename, extension);
                    Response.SetContentTypeByExtesion(extension);
                    Response.End();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Request.QueryString["method"]))
                    {
                        Response.WriteEmbededFile("index", "html");
                        Response.SetContentTypeByExtesion("html");
                        Response.End();
                    }
                    else
                    {
                        var methodName = Request.QueryString["method"];
                        var method = this.GetType().GetMethod(methodName);
                        method.Invoke(this, null);
                    }
                }
                
            }
        }


        public void GetErrors()
        {
            IUnhandledErrorRepository rep = RepositoryFactory.Instance.CreateInstance<IUnhandledErrorRepository>();
            List<UnhandledError> errors = rep.GetAll();
            Response.RespondObjectAsJson(errors);            
        }

        public void GetErrorDetails()
        {
            IUnhandledErrorRepository rep = RepositoryFactory.Instance.CreateInstance<IUnhandledErrorRepository>();
            string id = Request.QueryString["id"];
            UnhandledError ue = rep.GetById(id);
            Response.RespondObjectAsJson(ue);      
        }


        public void GetCookieList()
        {
            IUnhandledCookieRepository rep = RepositoryFactory.Instance.CreateInstance<IUnhandledCookieRepository>();
            string idError = Request.QueryString["idError"];
            List<UnhandledCookie> ucs = rep.GetByErrorId(Guid.Parse(idError));
            Response.RespondObjectAsJson(new ListReturnWrapper<List<UnhandledCookie>>(ucs));
        }
       
           
    }
}
