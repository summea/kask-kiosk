﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Kask.Services.Exceptions
{
    [DataContract(Name="KaskServiceType")]
    public enum ServiceExceptionType
    {
        [EnumMember]
        UNAUTHORIZED = 0x01,
        [EnumMember]
        INVALIDID = 0x02,
        [EnumMember]
        UNKNOWN = 0xff
    };

    [DataContract]
    public class KaskServiceException
    {
        public KaskServiceException() { }
    }
}
