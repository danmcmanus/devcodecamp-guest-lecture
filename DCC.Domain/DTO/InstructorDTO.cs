using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.Domain.DTO
{
    public class InstructorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Image { get; set; }
        public double? AverageRating { get; set; }
        public int AggregateRatings { get; set; }
        public int NumberOfRatings { get; set; }
        public string FullName => $"{FirstName} {LastName}";

    }
}
