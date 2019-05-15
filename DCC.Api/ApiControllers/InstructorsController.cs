using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCC.Domain.Models;
using DCC.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DCC.Api.ApiControllers
{
    [Route("api/instructors")]
    public class InstructorsController : Controller
    {
        private readonly IInstructorsService _instructorsService;

        public InstructorsController(IInstructorsService instructorsService)
        {
            _instructorsService = instructorsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetInstructors()
        {
            var response = await _instructorsService.GetAllInstructorsAsync();
            return Ok(response);
        }

        [HttpGet("{instructorId}")]
        public async Task<IActionResult> GetInstructorById(int instructorId)
        {
            var response = await _instructorsService.GetInstructorByIdAsync(instructorId);
            return Ok(response);
        }
        
        
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

        [HttpPut("update")]
        public async Task<IActionResult> UpdateInstructorData([FromBody]UpdateInstructorRequest request)
        {
            var response = await _instructorsService.UpdateInstructorAsync(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("rate")]
        public async Task<IActionResult> RateInstructor([FromBody] RateInstructorRequest request)
        {
            var response = await _instructorsService.RateInstructorAsync(request);
            return Ok(response);
        }

        [HttpDelete("{instructorId}")]
        public async Task<IActionResult> Delete(int instructorId)
        {
            var response = await _instructorsService.DeleteInstructorAsync(instructorId);
            return Ok(response);
        }
    }
}
