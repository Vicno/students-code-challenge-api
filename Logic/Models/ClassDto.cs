using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class ClassDto
    {
        public string ClassCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<StudentDto> Students { get; set; }
    }
}

