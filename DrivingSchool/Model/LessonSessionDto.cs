using DrivingSchool.Domain.Entities;
using DrivingSchool.Domain.Enum;

namespace DrivingSchool.Model
{
    public class LessonSessionDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ClientId { get; set; }
        public User Client { get; set; }
        public int InstructorId { get; set; }
        public User Instructor { get; set; }
        public Status Status { get; set; }
    }
}
