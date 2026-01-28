using Clinic.Core.Services;
using ClinicAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Clinic.Core.DTOs.PatientDtos;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: api/<PatientsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientResponseDto>>> GetAll()
        {
            var patients = await _patientService.GetAllAsync();
            return Ok(patients);
        }

        // GET: api/<PatientsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientResponseDto>> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound("המטופל לא נמצא");
            return Ok(patient);
        }

        // POST: api/<PatientsController>
        [HttpPost]
        public async Task<ActionResult<PatientResponseDto>> Create(PatientRequestDto patientDto)
        {
            try
            {
                var result = await _patientService.AddAsync(patientDto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/<PatientsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PatientRequestDto patientDto)
        {
            try
            {
                await _patientService.UpdateAsync(id, patientDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/<PatientsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _patientService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

