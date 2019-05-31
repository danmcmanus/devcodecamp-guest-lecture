

using Microsoft.AspNetCore.Http;

namespace DCC.Data.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Image { get; set; }
        public decimal AverageRating { get; set; }
        public int AggregateRatings { get; set; }
        public int NumberOfRatings { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class InstructorVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Image { get; set; }
    }
}   
