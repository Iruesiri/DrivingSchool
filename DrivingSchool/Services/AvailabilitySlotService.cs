using DrivingSchool.Application.Interfaces;
using DrivingSchool.Domain.Entities;
using DrivingSchool.Model;

namespace DrivingSchool.Services
{
    public interface IAvailabilitySlotService
    {
        Task<Response<AvailabilitySlot>> CreateAvailability(AvailabilitySlotDto request);
        Task<List<AvailabilitySlot>> GetAllAsync();
        Task<AvailabilitySlot> GetAvailabilityByDriverIdAsync(int driverId);
        Task<AvailabilitySlot> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, AvailabilitySlotDto request);
    }
    public class AvailabilitySlotService : IAvailabilitySlotService
    {
        private readonly IRepository<AvailabilitySlot> _availabilitySlotRepository;

        public AvailabilitySlotService(IRepository<AvailabilitySlot> availabilitySlotRepository,
        IJwtTokenService jwtService, IConfiguration configuration)
        {
            _availabilitySlotRepository = availabilitySlotRepository;
        }

        public async Task<Response<AvailabilitySlot>> CreateAvailability(AvailabilitySlotDto request)
        {
            if(request.StartTime < DateTime.Now || request.StartTime >= request.EndTime)
            {
                return new Response<AvailabilitySlot>
                {
                    Success = false,
                    Message = "Start time must be accurate."
                };
            }

            var availability = new AvailabilitySlot
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                InstructorId = request.InstructorId
            };

            await _availabilitySlotRepository.AddAsync(availability);
            return new Response<AvailabilitySlot> { Success = true , Message = "Availability slot created successfully", Data = availability };
        }

        public async Task DeleteAsync(int id)
        {
            var availability = await _availabilitySlotRepository.GetByIdAsync(id);
            if (availability != null)
            {
                await _availabilitySlotRepository.DeleteAsync(availability);
            }
            else
            {
                throw new Exception("Availability slot not found.");
            }
        }

        public async Task<List<AvailabilitySlot>> GetAllAsync()
        {
            return await _availabilitySlotRepository.GetAllAsync();
        }

        public async Task<AvailabilitySlot> GetAvailabilityByDriverIdAsync(int driverId)
        {
            return await _availabilitySlotRepository.FirstOrDefaultAsync(x => x.InstructorId == driverId);
        }

        public async Task<AvailabilitySlot> GetByIdAsync(int id)
        {
            return await _availabilitySlotRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, AvailabilitySlotDto request)
        {
            var availability = await _availabilitySlotRepository.GetByIdAsync(id);
            if (availability != null)
            {
                availability.StartTime = request.StartTime;
                availability.EndTime = request.EndTime;

                await _availabilitySlotRepository.UpdateAsync(availability);
            }
            else
            {
                throw new Exception("Availability slot not found.");
            }
        }
    }
}
