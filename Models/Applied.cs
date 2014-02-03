using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empty.Models
{
    public class Applied
    {
        public int Applicant_ID { get; set; }
        public int Application_ID { get; set; }
        public int JobID { get; set; }
        public DateTime dateApplied { get; set; }
    }
}
