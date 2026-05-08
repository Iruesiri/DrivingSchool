using DrivingSchool.Model;
using DrivingSchool.Services;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AvailabilitySlotController : ControllerBase
    {
        private readonly IAvailabilitySlotService _schedulingService;
        public AvailabilitySlotController(IAvailabilitySlotService schedulingService)
        {
            _schedulingService = schedulingService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(AvailabilitySlotDto request)
        {
            var availability = await _schedulingService.CreateAvailability(request);
            if (availability is null)
                return BadRequest("An error occurred while creating availability");

            return Ok(availability);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var availabilitySlots = await _schedulingService.GetAllAsync();
            return Ok(availabilitySlots);
        }

        //Get driver availability
        [HttpGet("driver/{id}")]
        public async Task<ActionResult> GetAvailabilityByDriverId(int id)
        {
            var availability = await _schedulingService.GetAvailabilityByDriverIdAsync(id);
            if (availability == null)
                return NotFound($"Availability slot not found for driver with ID {id}");

            return Ok(availability);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var availability = await _schedulingService.GetByIdAsync(id);
            if (availability is null)
                return NotFound("Availability slot not found");

            return Ok(availability);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _schedulingService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, AvailabilitySlotDto request)
        {
            await _schedulingService.UpdateAsync(id, request);

            return Ok("Updated successfully");
        }
    }
}
