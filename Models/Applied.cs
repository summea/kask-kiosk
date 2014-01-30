using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empty.Models
{
    public class Applied
    {
        public int applicant_ID { get; set; }
        public int application_ID { get; set; }
        public int jobID { get; set; }
        //date type??
        public string dateApplied { get; set; }
    }
}