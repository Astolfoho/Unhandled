using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unhandled.Repository.Interfaces
{
    public interface IUnhandledCookieRepository
    {
        Models.Cookie Create(Models.Cookie sc);

        List<Models.Cookie> GetByErrorId(long id);
    }
}
