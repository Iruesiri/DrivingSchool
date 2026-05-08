using DrivingSchool.Application.Interfaces;
using DrivingSchool.Domain.Entities;
using DrivingSchool.Model;

namespace DrivingSchool.Services
{
    public interface ISchedulingService
    {
        Task<Response<LessonSession>> ScheduleSession(LessonSessionDto request);
        Task<List<LessonSession>> GetAllAsync();
        Task<List<LessonSession>> GetSessionsByDriverIdAsync(int driverId);
        Task<List<LessonSession>> GetSessionByDriverIdAsync(int driverId, DateTime requestedStart, DateTime requestedEnd);
        Task<LessonSession> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, LessonSessionDto request);
    }
    public class SchedulingService : ISchedulingService
    {
        private readonly IRepository<AvailabilitySlot> _availabilitySlotRepository;
        private readonly IRepository<LessonSession> _lessonSessionRepository;

        public SchedulingService(IRepository<AvailabilitySlot> availabilitySlotRepository, IRepository<LessonSession> lessonSessionRepository, IConfiguration configuration)
        {
            _availabilitySlotRepository = availabilitySlotRepository;
            _lessonSessionRepository = lessonSessionRepository;
        }

        public async Task DeleteAsync(int id)
        {
            var session = await _lessonSessionRepository.GetByIdAsync(id);
            if (session != null)
            {
                await _lessonSessionRepository.DeleteAsync(session);
            }
            else
            {
                throw new Exception("Lesson session not found.");
            }
        }

        public async Task<List<LessonSession>> GetAllAsync()
        {
            return await _lessonSessionRepository.GetAllAsync();
        }

        public async Task<LessonSession> GetByIdAsync(int id)
        {
            return await _lessonSessionRepository.GetByIdAsync(id);
        }

        public async Task<List<LessonSession>> GetSessionsByDriverIdAsync(int driverId)
        {
            return await _lessonSessionRepository.FindAsync(x => x.InstructorId == driverId);
        }

        public async Task<List<LessonSession>> GetSessionByDriverIdAsync(int driverId, DateTime requestedStart, DateTime requestedEnd)
        {
            return await _lessonSessionRepository.FindAsync(s => s.InstructorId == driverId && s.StartTime < requestedEnd && s.EndTime > requestedStart);
        }

        public async Task<Response<LessonSession>> ScheduleSession(LessonSessionDto request)
        {
            if (request.StartTime < DateTime.Now || request.StartTime >= request.EndTime)
            {
                return new Response<LessonSession>
                {
                    Success = false,
                    Message = "Start time must be accurate."
                };
            }

            LessonSession session = new LessonSession
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                InstructorId = request.InstructorId,
                ClientId = request.ClientId,
                Status = request.Status
            };
            await _lessonSessionRepository.AddAsync(session);
            return new Response<LessonSession> { Success = true, Message = "Lesson session created successfully", Data = session };
        }

        public async Task UpdateAsync(int id, LessonSessionDto request)
        {
            var session = await _lessonSessionRepository.GetByIdAsync(id);
            if (session != null)
            {
                session.StartTime = request.StartTime;
                session.EndTime = request.EndTime;
                session.InstructorId = request.InstructorId;
                session.ClientId = request.ClientId;
                session.Status = request.Status;

                await _lessonSessionRepository.UpdateAsync(session);
            }
            else
            {
                throw new Exception("Lesson session not found.");
            }
        }
    }
}
