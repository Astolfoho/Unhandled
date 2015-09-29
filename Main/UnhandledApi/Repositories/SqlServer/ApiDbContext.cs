using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhandledApi.Models;

namespace UnhandledApi.Repositories.SqlServer
{
    public class ApiDbContext : System.Data.Entity.DbContext
    {
        public ApiDbContext() :base("SqlAzure")
        {

        }


        public System.Data.Entity.DbSet<Application> Applications { get; set; }

        public System.Data.Entity.DbSet<Error> Errors { get; set; }

        public System.Data.Entity.DbSet<Cookie> Cookies { get; set; }

    }
}
