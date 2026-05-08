using DrivingSchool.Domain.Entities;
using DrivingSchool.Model;
using DrivingSchool.Services;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SchedulingController : ControllerBase
    {
        private readonly ISchedulingService _schedulingService;
        public SchedulingController(ISchedulingService schedulingService)
        {
            _schedulingService = schedulingService;
        }

        [HttpPost]
        public async Task<ActionResult> ScheduleSession(LessonSessionDto request)
        {
            var lessonSession = await _schedulingService.ScheduleSession(request);
            if (lessonSession is null)
                return BadRequest("An error occurred while creating lesson session");
            return Ok(lessonSession);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var availabilitySlots = await _schedulingService.GetAllAsync();
            return Ok(availabilitySlots);
        }

        //Get driver availability
        [HttpGet("driver/{id}")]
        public async Task<ActionResult> GetSessionByDriverId(int id)
        {
            var session = await _schedulingService.GetSessionsByDriverIdAsync(id);
            if (session == null)
                return NotFound($"Session not found for driver with ID {id}");
            return Ok(session);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var session = await _schedulingService.GetByIdAsync(id);
            if (session is null)
                return NotFound("Session not found");

            return Ok(session);
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
        public async Task<ActionResult> Update(int id, LessonSessionDto request)
        {
            await _schedulingService.UpdateAsync(id, request);

            return Ok("Updated successfully");
        }
    }
}
