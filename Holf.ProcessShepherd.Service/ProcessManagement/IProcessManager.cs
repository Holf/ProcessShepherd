using Holf.ProcessShepherd.Service.Configuration;
using System;
using System.Collections.Generic;

namespace Holf.ProcessShepherd.Service.ProcessManagement
{
    public interface IProcessManager
    {
        void TerminateUnpermittedServices(ShepherdConfiguration shepherdConfiguration);
    }
}