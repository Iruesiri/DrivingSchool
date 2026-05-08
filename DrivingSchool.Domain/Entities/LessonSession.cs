using DrivingSchool.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingSchool.Domain.Entities
{
    public class LessonSession
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ClientId { get; set; }
        public User Client { get; set; }
        public int InstructorId { get; set; }
        public User Instructor { get; set; }
        public Status Status { get; set; }
    }
}
