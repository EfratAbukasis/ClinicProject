using Clinic.Core.DTOs;
using Clinic.Core.Services;
using Microsoft.AspNetCore.Mvc;


namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET api/<AppointmentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentResponseDto>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAsync();
            return Ok(appointments);
        }

        // GET api/<AppointmentsController>/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentResponseDto>> GetById(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null) return NotFound("התור לא נמצא");

            return Ok(appointment);
        }

        // POST api/<AppointmentsController>
        [HttpPost]
        public async Task<ActionResult<AppointmentResponseDto>> Create(AppointmentRequestDto appointmentDto)
        {
            try
            {
                var result = await _appointmentService.AddAsync(appointmentDto);

                // מחזיר 201 עם נתיב לשליפת התור החדש
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                // כאן נתפסת השגיאה של "לא ניתן לקבוע תור לעבר"
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AppointmentsController>/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AppointmentRequestDto appointmentDto)
        {
            try
            {
                await _appointmentService.UpdateAsync(id, appointmentDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<AppointmentsController>/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _appointmentService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}