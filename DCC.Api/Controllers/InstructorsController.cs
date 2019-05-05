using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCC.Domain.Models;
using DCC.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DCC.Api.Controllers
{
    [Route("api/instructors")]
    public class InstructorsController : Controller
    {
        private readonly IInstructorsService _instructorsService;

        public InstructorsController(IInstructorsService instructorsService)
        {
            _instructorsService = instructorsService;
        }
        // GET: api/instructors
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetInstructors()
        {
            var response = await _instructorsService.GetAllInstructorsAsync();
            return Ok(response);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddInstructor([FromBody]InstructorRequest request)
        {
            var response = await _instructorsService.AddInstructorAsync(request);
            if (response.IsError)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPut]
        [Route("rate")]
        public async Task<IActionResult> RateInstructor([FromBody] RateInstructorRequest request)
        {
            var response = await _instructorsService.RateInstructorAsync(request);
            return Ok(response);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
