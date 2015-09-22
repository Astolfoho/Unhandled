using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unhandled.Repository.Data;
using Unhandled.Repository.Interfaces;

namespace Unhandled.Repository.LocalSql
{
    public class UnhandledCookieItemRepository : IUnhandledCookieRepository
    {
        public Models.Cookie Create(Models.Cookie sc)
        {
            using (var db = DbFactory.CreateConnection("unhandled.UnhandledCookieRepository_Create"))
            {
                db.EnsureParameter(sc);
                db.ExecuteNonQuery();
                sc.Id = db.ExecuteScalar<long>();
            }
            return sc;
        }

        public List<Models.Cookie> GetByErrorId(long guid)
        {
            using (var db = DbFactory.CreateConnection("unhandled.UnhandledCookieRepository_GetByErrorId"))
            {
                db.EnsureParameter("ErrorId", guid);
                using (var reader = db.ExecuteReader())
                {
                    return reader.MapList<Models.Cookie>();
                }
            }
        }
    }
}
