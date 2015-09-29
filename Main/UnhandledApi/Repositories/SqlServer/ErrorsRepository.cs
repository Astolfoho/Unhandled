using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhandledApi.Models;
using UnhandledApi.Repositories.Interfaces;

namespace UnhandledApi.Repositories.SqlServer
{
    public class ErrorsRepository : BaseRepository, IErrorsRepository
    {
        

       
        public Error Create(Error error)
        {
            error = DbContext.Errors.Add(error);
            DbContext .SaveChanges();
            return error;
        }

        public bool DeleteById(long id)
        {
            var e = new Error { Id = id };
            DbContext.Errors.Attach(e);
            DbContext.Errors.Remove(e);
            DbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Error> GetAll()
        {
            return DbContext.Errors.ToList();
        }

        public List<Error> GetByApplicationId(long id)
        {
            return DbContext.Errors.Where(s => s.ApplicationId == id && !s.ParentErrorId.HasValue).ToList();
        }

        public Error GetById(long id)
        {

            var error = (from e in DbContext.Errors
                         join ie in DbContext.Errors on e.Id equals ie.ParentErrorId into _ie
                         from ie in _ie.DefaultIfEmpty()
                         where e.Id == id
                         select new { e = e, ie = ie})
                         .ToList()
                         .Select(s => new Error(s.e, s.ie != null ? s.ie.Id : 0 ))
                         .FirstOrDefault();

            return error;
        }
    }
}
