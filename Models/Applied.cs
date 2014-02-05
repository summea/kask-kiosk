using System;

namespace Kask.DAL.Models
{
    public class Applied
    {
        public int Applicant_ID { get; set; }
        public int Application_ID { get; set; }
        public int JobID { get; set; }
        public DateTime DateApplied { get; set; }
    }
}
