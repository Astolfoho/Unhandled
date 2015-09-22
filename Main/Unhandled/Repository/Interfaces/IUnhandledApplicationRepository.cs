using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unhandled.Models;

namespace Unhandled.Repository.Interfaces
{
    public interface IUnhandledApplicationRepository
    {
        Application Create(Application app);

        Application GetOrCreate();

        Application GetByMachineNameAndApplicationName(string machineName, string siteName);
    }
}
