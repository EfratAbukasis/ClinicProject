using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Repositories;
using Clinic.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Service
{
    internal class DoctorService : IDoctorService
    {
        private readonly IGenericRepository<Doctor> _doctorRepository;
        

        public DoctorService(IGenericRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetAllAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync(d => d.Appointments);

            // המרה מישות ל-ResponseDto
            return doctors.Select(d => new DoctorResponseDto
            {
                Id = d.Id,
                FullName = $"{d.FirstName} {d.LastName}",
                LicenseNumber = d.LicenseNumber,
                Specialization = d.Specialty
            });
        }

        public async Task<DoctorResponseDto> GetByIdAsync(int id)
        {
            var d = await _doctorRepository.GetByIdAsync(id, d => d.Appointments);
            if (d == null) return null;

            return new DoctorResponseDto
            {
                Id = d.Id,
                FullName = $"{d.FirstName} {d.LastName}",
                LicenseNumber = d.LicenseNumber,
                Specialization = d.Specialty
            };
        }

        public async Task<DoctorResponseDto> AddAsync(DoctorRequestDto doctorDto)
        {
            if (string.IsNullOrWhiteSpace(doctorDto.LicenseNumber))
                throw new ArgumentException("מספר רישיון הוא שדה חובה");

            // בדיקה אם כבר קיים רופא עם אותו מספר רישיון
            var allDoctors = await _doctorRepository.GetAllAsync();
            if (allDoctors.Any(d => d.LicenseNumber == doctorDto.LicenseNumber))
            {
                throw new InvalidOperationException("רופא עם מספר רישיון זה כבר קיים במערכת");
            }

            var doctorEntity = new Doctor
            {
                FirstName = doctorDto.FirstName,
                LastName = doctorDto.LastName,
                LicenseNumber = doctorDto.LicenseNumber,
                Specialty = doctorDto.Specialization
            };

            //  DB - שמירה ב
            var savedDoctor = await _doctorRepository.AddAsync(doctorEntity);

            // ResponseDto - החזרה כ 
            return new DoctorResponseDto
            {
                Id = savedDoctor.Id,
                FullName = $"{savedDoctor.FirstName} {savedDoctor.LastName}",
                LicenseNumber = savedDoctor.LicenseNumber,
                Specialization = savedDoctor.Specialty
            };
        }

        public async Task UpdateAsync(int id, DoctorRequestDto doctorDto)
        {
            // בדיקה שהרופא קיים במערכת לפני העדכון
            var existingDoctor = await _doctorRepository.GetByIdAsync(id);
            if (existingDoctor == null)
            {
                throw new KeyNotFoundException($"לא נמצא רופא עם מזהה {id}");
            }

            //DTO - עדכון שדות הישות מה
            existingDoctor.FirstName = doctorDto.FirstName;
            existingDoctor.LastName = doctorDto.LastName;
            existingDoctor.LicenseNumber = doctorDto.LicenseNumber;
            existingDoctor.Specialty = doctorDto.Specialization;

            await _doctorRepository.UpdateAsync(id, existingDoctor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctorWithAppointments = await _doctorRepository.GetByIdAsync(id, d => d.Appointments);

            if (doctorWithAppointments == null) return false;

            if (doctorWithAppointments.Appointments.Any(a => a.StartTime > DateTime.Now))
            {
                throw new InvalidOperationException("לא ניתן למחוק רופא שיש לו תורים עתידיים, יש לבטל או להעביר את התורים קודם לכן");
            }

            return await _doctorRepository.RemoveAsync(id);
        }
    }

}

