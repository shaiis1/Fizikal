using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizikalTest_Shai.Models
{
    public class Customers
    {
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public long[] phoneNumbers { get; set; }

        public int cityId { get; set; }

        public float CallsTotalTime { get; set; }

        public List<Calls> Calls { get; set; }
    }
}
