using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Exceptions;
using Clinic.Core.Repositories;
using Clinic.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Service
{
    internal class AppointmentService : IAppointmentService
    {
        private readonly IGenericRepository<Appointment> _appointmentRepository;

        public AppointmentService(IGenericRepository<Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<IEnumerable<AppointmentResponseDto>> GetAllAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync(a => a.Patient, a => a.Doctor);

            return appointments.Select(a => new AppointmentResponseDto
            {
                Id = a.Id,
                StartTime = a.StartTime,
                PatientName = $"{a.Patient.FirstName} {a.Patient.LastName}",
                DoctorName = $"{a.Doctor.FirstName} {a.Doctor.LastName}",
                Specialty = a.Specialty
            });
        }
        public async Task<AppointmentResponseDto> GetByIdAsync(int id)
        {
            var a = await _appointmentRepository.GetByIdAsync(id, a => a.Patient, a => a.Doctor);
            if (a == null) return null;

            return new AppointmentResponseDto
            {
                Id = a.Id,
                StartTime = a.StartTime,
                PatientName = $"{a.Patient.FirstName} {a.Patient.LastName}",
                DoctorName = $"{a.Doctor.FirstName} {a.Doctor.LastName}",
                Specialty = a.Specialty
            };
        }
        public async Task<AppointmentResponseDto> AddAsync(AppointmentRequestDto appointmentDto)
        {
            if (appointmentDto.StartTime < System.DateTime.Now)
            {
                throw new InvalidAppointmentTimeException(appointmentDto.StartTime);
            }

            var appointment = new Appointment
            {
                PatientId = appointmentDto.PatientId,
                DoctorId = appointmentDto.DoctorId,
                StartTime = appointmentDto.StartTime,
                Specialty = (string)appointmentDto.Specialty
            };

            //  שמירה
            var saved = await _appointmentRepository.AddAsync(appointment);
            return await GetByIdAsync(saved.Id);
        }
        public async Task UpdateAsync(int id, AppointmentRequestDto appointmentDto)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if (existingAppointment == null)
            {
                throw new Exception("התור חא נמצא");
            }
            if (appointmentDto.StartTime < DateTime.Now)
            {
                throw new InvalidAppointmentTimeException(appointmentDto.StartTime);
            }
            existingAppointment.DoctorId = appointmentDto.DoctorId;
            existingAppointment.PatientId = appointmentDto.PatientId;
            existingAppointment.StartTime = appointmentDto.StartTime;
            existingAppointment.Specialty = (string)appointmentDto.Specialty;

            await _appointmentRepository.UpdateAsync(id, existingAppointment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _appointmentRepository.RemoveAsync(id);
        }
    }
}

    

