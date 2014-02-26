using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Kask.Services.Exceptions
{
    [DataContract]
    public class KaskServiceExceptions
    {
        public KaskServiceExceptions(string m)
        {
            ExceptionMessage = m;
        }

        [DataMember]
        public string ExceptionMessage { get; set; }
    }
}