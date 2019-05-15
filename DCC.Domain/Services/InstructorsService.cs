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
            var instructor = await _context.FindAsync<Instructor>(instructorId);
            instructor.AverageRating =
                this.CalculateInstructorRatingAsync(instructor.AggregateRatings, instructor.NumberOfRatings);
            return instructor;
        }

        public async Task<IEnumerable<Instructor>> SearchInstructorsByNameAsync(string name)
        {
            var instructors = await _context.Instructors.Where(i => $"{i.FirstName} {i.LastName}".Contains(name)).ToListAsync();
            return instructors;
        }

        public async Task<InstructorResponse> AddInstructorAsync(InstructorRequest request)
        {
            var response = new InstructorResponse { IsError = false };
            try
            {
                await _context.Instructors.AddAsync(new Instructor
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Image = request.Image,
                    JobTitle = request.JobTitle
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public async Task<UpdateInstructorResponse> UpdateInstructorAsync(UpdateInstructorRequest request)
        {
            var response = new UpdateInstructorResponse { IsError = false };

            try
            {
                var instructorToUpdate = await _context.Instructors.Where(i => i.Id == request.InstructorId).SingleOrDefaultAsync();

                if (instructorToUpdate != null)
                {
                    instructorToUpdate.FirstName = request.FirstName != instructorToUpdate.FirstName ? request.FirstName : instructorToUpdate.FirstName;
                    instructorToUpdate.LastName = request.LastName != instructorToUpdate.LastName ? request.LastName : instructorToUpdate.LastName;
                    instructorToUpdate.JobTitle = request.JobTitle != instructorToUpdate.JobTitle ? request.JobTitle : instructorToUpdate.JobTitle;
                    instructorToUpdate.Image = request.Image != instructorToUpdate.Image ? request.Image : instructorToUpdate.Image;

                    _context.Instructors.Update(instructorToUpdate);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    response.FirstName = instructorToUpdate.FirstName;
                    response.LastName = instructorToUpdate.LastName;
                    response.JobTitle = instructorToUpdate.JobTitle;
                    response.NumberOfRatings = instructorToUpdate.NumberOfRatings;
                    response.Image = instructorToUpdate.Image;
                    response.AggregateRatings = instructorToUpdate.AggregateRatings;
                    response.AverageRating = instructorToUpdate.AverageRating;
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseApiResponse> RateInstructorAsync(RateInstructorRequest request)
        {
            var response = new BaseApiResponse { IsError = false };
            try
            {
                var instructorToRate = await _context.Instructors.SingleOrDefaultAsync(i => i.Id == request.InstructorId);
                instructorToRate.NumberOfRatings++;
                instructorToRate.AggregateRatings += request.Rating;
                instructorToRate.AverageRating = CalculateInstructorRatingAsync(instructorToRate.AggregateRatings, instructorToRate.NumberOfRatings);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.ErrorMessage = e.Message;
            }

            return response;
        }


        private decimal CalculateInstructorRatingAsync(decimal aggregateRatings, int totalRatingsCount)
        {
            return aggregateRatings / totalRatingsCount;
        }

        public async Task<BaseApiResponse> DeleteInstructorAsync(int instructorId)
        {
            var response = new BaseApiResponse { IsError = false };

            try
            {
                var instructorToDelete = await _context.Instructors.SingleOrDefaultAsync(i => i.Id == instructorId);
                if (instructorToDelete != null)
                {
                    instructorToDelete.IsDeleted = true;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
