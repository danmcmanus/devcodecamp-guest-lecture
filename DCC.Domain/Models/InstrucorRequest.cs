using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.Domain.Models
{
    public class InstructorRequest
    {
        public string Name { get; set; }
        public string InstructorBio { get; set; }
        public string Image { get; set; }
        public string Position { get; internal set; }
    }
}
