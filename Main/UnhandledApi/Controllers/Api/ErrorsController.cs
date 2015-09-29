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
    [DefaultRouteName("ErrorsRoute")]
    public class ErrorsController : BaseApiController
    {
        private IErrorsRepository _rep;

        public ErrorsController(IErrorsRepository rep)
        {          
            _rep = rep;
        }
        
        public IHttpActionResult Get()
        {

            try
            {
                return Ok(_rep.GetAll());
            }
            catch (Exception)
            {

                return InternalServerError();
            }
            
        }

        public IHttpActionResult Get(long id)
        {
            try
            {
                return Ok(_rep.GetById(id));
            }
            catch (Exception)
            {

                return InternalServerError();
            }

        }

        [Route("api/Applications/{applicationId}/Errors")]
        public IHttpActionResult GetByAppId([FromUri]long applicationId)
        {
            try
            {
                var errors = _rep.GetByApplicationId(applicationId);

                if(errors == null || !errors.Any())
                {
                    return NotFound();
                }

                return Ok(errors);
            }
            catch (Exception)
            {

                return InternalServerError();
            }

        }

        [Route("api/Applications/{applicationId}/Errors/{id}")]
        public IHttpActionResult GetByAppId(long applicationId, long id)
        {
            try
            {
                var error = _rep.GetById(id);

                if (error == null || error.ApplicationId != id)
                {
                    return NotFound();
                }

                return Ok(error);
            }
            catch (Exception)
            {

                return InternalServerError();
            }

        }


        public IHttpActionResult Post([FromBody]Error error)
        {
            try
            {
                return Created(_rep.Create(error));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Delete(long id)
        {
            try
            {
                _rep.DeleteById(id);
                return Deleted();
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }
    }
}
