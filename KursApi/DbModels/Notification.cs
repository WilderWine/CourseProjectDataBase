using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModels
{
    public class Notification
    {
        public int n_id { get; set; }
        public int u_id { get; set; }
        public String content { get; set; }
        public DateTime? postdate { get; set; }
    }
}
