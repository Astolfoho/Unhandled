using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnhandledApi.Models;
using UnhandledApi.Repositories.Interfaces;

namespace UnhandledApi.Controllers.Api
{
    public class ErrorsController : ApiController
    {
        private IErrorsRepository _rep;

        public ErrorsController(IErrorsRepository rep)
        {
            _rep = rep;
        }
        
        public IEnumerable<Error> Get()
        {
            return _rep.GetAll();
        }

        public Error Get(long id)
        {
            return _rep.GetById(id);
        }

        public Error Post(Error error)
        {
            return _rep.Add(error);
        }

        public Error Delete(long id)
        {
            return _rep.DeleteById(id);
        }
    }
}
