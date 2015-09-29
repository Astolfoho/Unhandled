using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhandledApi.Models;

namespace UnhandledApi.Repositories.Interfaces
{
    public interface IErrorsRepository : IRepository
    {
        IEnumerable<Error> GetAll();
        Error GetById(long id);
        Error Create(Error error);
        bool DeleteById(long id);
        List<Error> GetByApplicationId(long id);
    }
}
