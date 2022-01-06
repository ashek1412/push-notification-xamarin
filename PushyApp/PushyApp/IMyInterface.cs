using System;
using System.Collections.Generic;
using System.Text;

namespace PushyApp
{
    public interface IMyInterface
    {
        string GetPlatformName();

        int StartService();

        int StopService();

    }
}
