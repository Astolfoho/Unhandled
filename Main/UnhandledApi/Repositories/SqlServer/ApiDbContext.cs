using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnhandledApi.Models
{
    public class ApiDbContext : System.Data.Entity.DbContext
    {
        public ApiDbContext() :base("Default")
        {

        }


        public System.Data.Entity.DbSet<Error> Errors { get; set; }

        public System.Data.Entity.DbSet<Cookie> Cookies { get; set; }

    }
}
