﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unhandled.Models;

namespace Unhandled.Repository.Interfaces
{
    public interface IUnhandledErrorRepository
    {
        UnhandledError Create(UnhandledError uh);

        UnhandledError GetById(long id);

        List<Models.UnhandledError> GetAll();

        List<Models.UnhandledError> GetMainErrors();
    }
}
