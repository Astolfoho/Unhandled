using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhandledApi.Models;

namespace UnhandledApi.Repositories.Interfaces
{
    public interface IErrorsRepository
    {
        IEnumerable<Error> GetAll();
        Error GetById(long id);
        Error Add(Error error);
        Error DeleteById(long id);
    }
}
