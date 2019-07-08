using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCC.Data.Models;
using DCC.Domain.Models;

namespace DCC.Domain.Services
{
    public interface IInstructorsService
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
        Task<Instructor> GetInstructorByIdAsync(int instructorId);
        Task<IEnumerable<Instructor>> SearchInstructorsByNameAsync(string name);
        Task<InstructorResponse> AddInstructorAsync(InstructorRequest request);
        Task<UpdateInstructorResponse> UpdateInstructorAsync(UpdateInstructorRequest request);
        Task<BaseApiResponse> RateInstructorAsync(RateInstructorRequest request);
        Task<BaseApiResponse> DeleteInstructorAsync(int instructorId);
        Task ProcessAsync();
    }
}
