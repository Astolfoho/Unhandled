using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnhandledApi.Repositories.SqlServer
{
    public class BaseRepository
    {
        protected ApiDbContext DbContext { get; private set; }

        public BaseRepository()
        {
            DbContext = new ApiDbContext();
        }
    }
}
