using DrivingSchool.Domain.Entities;

namespace DrivingSchool.Model
{
    public class AvailabilitySlotDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int InstructorId { get; set; }
    }
}
