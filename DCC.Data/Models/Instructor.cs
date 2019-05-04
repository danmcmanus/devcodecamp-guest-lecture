

namespace DCC.Data.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? AverageRating { get; set; }
        public int AggregateRatings { get; set; }
        public int NumberOfRatings { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public string InstructorBio { get; set; }
        public bool IsDeleted { get; set; }
    }
}
