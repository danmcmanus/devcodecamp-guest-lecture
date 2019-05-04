using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCC.Data;
using DCC.Data.Models;
using DCC.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DCC.Domain.Services
{
    public class InstructorsService : IInstructorsService
    {
        private readonly DCCDbContext _context;

        public InstructorsService(DCCDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync()
        {
            var instructors = await _context.Instructors.Where(i => !i.IsDeleted).ToListAsync();
            return instructors;
        }

        public async Task<Instructor> GetInstructorByIdAsync(int instructorId)
        {
            return await _context.FindAsync<Instructor>(instructorId);
        }

        public async Task<IEnumerable<Instructor>> SearchInstructorsByNameAsync(string name)
        {
            var instructors = await _context.Instructors.Where(i => i.Name.Contains(name)).ToListAsync();
            return instructors;
        }

        public async Task<InstructorResponse> AddInstructorAsync(InstructorRequest request)
        {
            var response = new InstructorResponse();
            try
            {
                await _context.Instructors.AddAsync(new Instructor
                {
                    Name = request.Name,
                    InstructorBio = request.InstructorBio,
                    Image = request.Image,
                    Position = request.Position
                });

                response.IsError = false;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public async Task<Instructor> UpdateInstructorAsync(UpdateInstructorRequest request)
        {
            var instructorToUpdate =
                await _context.Instructors.Where(i => i.Id == request.InstructorId).SingleOrDefaultAsync();

            if (instructorToUpdate != null)
            {
                instructorToUpdate.Image = request.Image;
                instructorToUpdate.Name = request.Name;
                instructorToUpdate.InstructorBio = request.InstructorBio;
                instructorToUpdate.Position = request.InstructorBio;

                _context.Instructors.Update(instructorToUpdate);
                await _context.SaveChangesAsync();
            }

            return instructorToUpdate;
        }

        public async Task<BaseApiResponse> RateInstructorAsync(RateInstructorRequest request)
        {
            var response = new BaseApiResponse();
            try
            {
                var instructorToRate = await _context.Instructors.SingleOrDefaultAsync(i => i.Id == request.InstructorId);
                instructorToRate.NumberOfRatings++;
                instructorToRate.AggregateRatings += request.Rating;
                instructorToRate.AverageRating = CalculateInstructorRatingAsync(instructorToRate.AggregateRatings,
                    instructorToRate.NumberOfRatings);
                await _context.SaveChangesAsync();
                response.IsError = false;
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.ErrorMessage = e.Message;
            }

            return response;
        }


        private double CalculateInstructorRatingAsync(int aggregateRatings, int totalRatingsCount)
        {
            return (double)aggregateRatings / totalRatingsCount;
        }
    }
}
