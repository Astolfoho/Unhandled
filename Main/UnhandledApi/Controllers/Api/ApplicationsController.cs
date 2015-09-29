using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnhandledApi.Base;
using UnhandledApi.Base.Attributes;
using UnhandledApi.Models;
using UnhandledApi.Repositories.Interfaces;

namespace UnhandledApi.Controllers.Api
{
    [DefaultRouteName("ApplicationsRoute")]
    public class ApplicationsController : BaseApiController
    {
        IApplicationRepository _rep;

        public ApplicationsController(IApplicationRepository rep)
        {
            _rep = rep;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_rep.GetAll());
            }
            catch (Exception ex)
            {

                return InternalServerError(ex); //todo
            }        
        }

        public IHttpActionResult Get(long id)
        {
            try
            {
                return Ok(_rep.GetById(id));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex); //todo
            }
        }

        public IHttpActionResult Get(string machineName, string applicationName)
        {
            try
            {
                return Ok(_rep.GetByMachineNameAndApplicationName(machineName, applicationName));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex); //todo
            }
        }

        public IHttpActionResult Post(Application app)
        {
            try
            {
                return Ok(_rep.Create(app));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex); //todo
            }
        }


    }
}
