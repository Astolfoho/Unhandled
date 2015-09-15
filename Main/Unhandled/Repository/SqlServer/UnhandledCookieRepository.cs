﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unhandled.Repository.Data;
using Unhandled.Repository.Interfaces;

namespace Unhandled.Repository.SqlServer
{
    public class UnhandledCookieItemRepository : IUnhandledCookieRepository
    {
        public Models.UnhandledCookie Create(Models.UnhandledCookie sc)
        {
            using (var db = DbFactory.CreateConnection("unhandled.UnhandledCookieRepository_Create"))
            {
                db.EnsureParameter(sc);
                db.ExecuteNonQuery();
                sc.Id = db.ExecuteScalar<long>();
            }
            return sc;
        }

        public List<Models.UnhandledCookie> GetByErrorId(long guid)
        {
            using (var db = DbFactory.CreateConnection("unhandled.UnhandledCookieRepository_GetByErrorId"))
            {
                db.EnsureParameter("ErrorId", guid);
                using (var reader = db.ExecuteReader())
                {
                    return reader.MapList<Models.UnhandledCookie>();
                }
            }
        }
    }
}