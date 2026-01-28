using Clinic.Core.DTOs;
using Clinic.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



    namespace ClinicAPI.Controllers
     {
        [ApiController]
        [Route("api/[controller]")]
        public class DoctorsController : ControllerBase
        {
            private readonly IDoctorService _doctorService;

            public DoctorsController(IDoctorService doctorService)
            {
                _doctorService = doctorService;
            }

        // GET: api/<DoctorsController>
        [HttpGet]
            public async Task<ActionResult<IEnumerable<DoctorResponseDto>>> GetAll()
            {
                var doctors = await _doctorService.GetAllAsync();
                return Ok(doctors);
            }

        // GET: api/<DoctorsController>/5
        [HttpGet("{id}")]
            public async Task<ActionResult<DoctorResponseDto>> GetById(int id)
            {
                var doctor = await _doctorService.GetByIdAsync(id);
                if (doctor == null) return NotFound("הרופא לא נמצא במערכת");
                return Ok(doctor);
            }

        // POST: api/<DoctorsController>
        [HttpPost]
            public async Task<ActionResult<DoctorResponseDto>> Create(DoctorRequestDto doctorDto)
            {
                try
                {
                    var result = await _doctorService.AddAsync(doctorDto);
                    return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        // PUT: api/<DoctorsController>/5
        [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, DoctorRequestDto doctorDto)
            {
                try
                {
                    await _doctorService.UpdateAsync(id, doctorDto);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        // DELETE: api/<DoctorsController>/5
        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    var success = await _doctorService.DeleteAsync(id);
                    if (!success) return NotFound();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }