using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empty.Models
{
    public class Applicant
    {
        public int Pk { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int SSN { get; set; }
        public char gender { get; set; }
    }
}