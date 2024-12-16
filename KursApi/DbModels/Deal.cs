using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModels
{
    public class Deal
    {
        public int d_id { get; set; }
        public int p_id { get; set; }
        public int u_id { get; set; }
        public DateTime? startterm { get; set; }
        public DateTime? endterm { get; set; }
        public String status { get; set; }
        public double debt { get; set; }
        public double loan { get; set; }
    }
}
