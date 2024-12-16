using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModels
{
    public class Auction
    {
        public int a_id { get; set; }
        public DateTime? startterm { get; set; }
        public DateTime? endterm { get; set; }
    }
}
