using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Hosting;
using Unhandled.Models;
using Unhandled.Repository.Data;
using Unhandled.Repository.Interfaces;

namespace Unhandled.Repository.LocalSql
{
    public class UnhandledErrorRepository : IUnhandledErrorRepository
    {

        public Models.Error Create(Models.Error uh)
        {
            using (var db = DbFactory.CreateConnection("unhandled.ErrorRepository_Create"))
            {
                db.EnsureParameter(uh);
                uh.Id = db.ExecuteScalar<long>();
            }
            return uh;
        }


        public Models.Error GetById(long id)
        {
            using (var db = DbFactory.CreateConnection("unhandled.ErrorRepository_GetById"))
            {
                db.EnsureParameter("Id", id);
                using (var reader = db.ExecuteReader())
                {
                    return reader.MapObject<Models.Error>();
                }
            }
        }

        public List<Models.Error> GetAll()
        {
            using (var db = DbFactory.CreateConnection("unhandled.ErrorRepository_GetAll"))
            {
                using (var reader = db.ExecuteReader())
                {
                    return reader.MapList<Models.Error>();       
                }
            }            
        }

        public List<Error> GetMainErrors()
        {
            using (var db = DbFactory.CreateConnection("unhandled.ErrorRepository_GetMainErrors"))
            {
                using (var reader = db.ExecuteReader())
                {
                    return reader.MapList<Models.Error>();
                }
            }
        }
    }
}
