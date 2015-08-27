using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unhandled.Repository.Interfaces
{
    public interface IUnhandledCookieRepository
    {
        Models.UnhandledCookie Create(Models.UnhandledCookie sc);

        List<Models.UnhandledCookie> GetByErrorId(Guid guid);
    }
}
