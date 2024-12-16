using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModels
{
    public class User
    {
        public int u_id { get; set; }
        public int r_id { get; set; }
        public String name { get; set; }
        public String login { get; set; }
        public String password { get; set; }
    }
}
