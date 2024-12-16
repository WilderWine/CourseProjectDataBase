using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModels
{
    public class UselLog
    {
        public int log_id { get; set; }
        public int u_id { get; set; }
        public String action { get; set; }
        public DateTime? actdate { get; set; }
    }
}
