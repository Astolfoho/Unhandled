using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unhandled.Models;

namespace Unhandled.Repository.Interfaces
{
    public interface IUnhandledErrorRepository
    {
        Error Create(Error uh);

        Error GetById(long id);

        List<Models.Error> GetAll();

        List<Models.Error> GetMainErrors();
    }
}
