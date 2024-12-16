using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModels
{
    public class Comment
    {
        public int com_id { get; set; }
        public int u_id { get; set; }
        public string content { get; set; }
        public DateTime postdate { get; set; }
    }
}
