using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhandledApi.Models;

namespace UnhandledApi.Repositories.Interfaces
{
    public interface IApplicationRepository : IRepository
    {
        List<Application> GetAll();

        Application GetById(long id);
        Application GetByMachineNameAndApplicationName(string machineName, string applicationName);
        Application Create(Application app);
    }
}
