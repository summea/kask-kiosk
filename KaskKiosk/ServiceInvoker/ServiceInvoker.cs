using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace KaskKiosk.Invoker
{
    public enum KaskServiceType
    {
        APPLICATION = 0,
        APPLICANT = 1,
        APPLIED = 2,
        UNKNOWN = 0xff
    };

    public static class ServiceInvoker
    {
        private static readonly Dictionary<Type, ClientBase<Type>> m_Services = new Dictionary<Type, ClientBase<Type>>();

        public static T Resolve<T>()
        {
            throw new NotImplementedException();
        }
    }
}