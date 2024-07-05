using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Class
    {
        public string ClassCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Students { get; set; }
    }
}
