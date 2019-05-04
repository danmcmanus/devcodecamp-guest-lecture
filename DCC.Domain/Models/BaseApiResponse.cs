using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.Domain.Models
{
    public class BaseApiResponse
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
