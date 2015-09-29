using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Hosting;
using Unhandled.Configuration;
using Unhandled.Models;
using Unhandled.Repository.Data;
using Unhandled.Repository.Interfaces;

namespace Unhandled.Repository.UnhandledApi
{
    public class UnhandledErrorRepository : IUnhandledErrorRepository
    {
        private HttpClient _client;

        public UnhandledErrorRepository()
        {
            _client = new HttpClient(UnhandledConfiguration.Current.ApiUrl, "Errors");
        }

        public Models.Error Create(Models.Error uh)
        {
            return _client.Post(uh);
        }

        public Models.Error GetById(long id)
        {
            return _client.Get<Models.Error>(id);
        }

        public List<Models.Error> GetAll()
        {
            return _client.Get<List<Models.Error>>();
        }

        public List<Error> GetMainErrors()  
        {
            var url = string.Format("{0}/Applications/{1}/Errors", 
                                    UnhandledConfiguration.Current.ApiUrl.TrimEnd('/'),
                                    Api.UnhandledApi.Instance.CurrentApplication.Id);
            return _client.Get<List<Models.Error>>(url);
        }
    }
}
