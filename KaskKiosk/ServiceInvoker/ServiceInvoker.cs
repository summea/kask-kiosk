using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace KaskKiosk.Invoker
{
    public enum KaskServiceType
    {
        APPLICATION = 0,
        APPLICANT = 1,
        APPLIED = 2,
        UNKNOWN = 0xff
    };

    public static class ServiceInvoker<T>
    {
    }
}