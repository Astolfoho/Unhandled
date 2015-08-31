using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Hosting;
using Unhandled.Repository.Data;
using Unhandled.Repository.Interfaces;

namespace Unhandled.Repository.LocalSql
{
    public class UnhandledErrorRepository : IUnhandledErrorRepository
    {

        public Models.UnhandledError Create(Models.UnhandledError uh)
        {
            using (var db = DbFactory.CreateConnection("unhandled.UnhandledErrorRepository_Create"))
            {
                db.EnsureParameter(uh);
                uh.Id = db.ExecuteScalar<long>();
            }
            return uh;
        }


        public Models.UnhandledError GetById(long id)
        {
            using (var db = DbFactory.CreateConnection("unhandled.UnhandledErrorRepository_GetById"))
            {
                db.EnsureParameter("Id", id);
                using (var reader = db.ExecuteReader())
                {
                    return reader.MapObject<Models.UnhandledError>();
                }
            }
        }

        public List<Models.UnhandledError> GetAll()
        {
            using (var db = DbFactory.CreateConnection("unhandled.UnhandledErrorRepository_GetAll"))
            {
                using (var reader = db.ExecuteReader())
                {
                    return reader.MapList<Models.UnhandledError>();       
                }
            }            
        }
    }
}
