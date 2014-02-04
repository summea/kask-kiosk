using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empty.Models
{
    public class Applicant
    {
        public int Applicant_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SSN { get; set; }
        public char Gender { get; set; }
    }
}
