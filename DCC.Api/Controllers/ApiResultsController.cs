using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DCC.Api.Controllers
{
    public class ApiResultsController : Controller
    {
        public IActionResult GetInstructors()
        {
            return View();
        }
    }
}
