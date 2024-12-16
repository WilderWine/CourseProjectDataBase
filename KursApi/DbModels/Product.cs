using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModels
{
    public class Product
    {
        public int p_id { get; set; }
        public int cat_id { get; set; }
        public int u_id { get; set; }
        public String status { get; set; }
        public String name { get; set; }
        public String description { get; set; }

    }
}
