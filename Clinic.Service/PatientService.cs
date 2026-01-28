using Clinic.Core.Entities;
using Clinic.Core.Repositories;
using Clinic.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Clinic.Core.DTOs.PatientDtos;

namespace Clinic.Service
{
    internal class PatientService : IPatientService
    {
        private readonly IGenericRepository<Patient> _patientRepository;

        public PatientService(IGenericRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<PatientResponseDto>> GetAllAsync()
        {
            // מחזיר את כל המטופלים כולל רשימת התורים שלהם
            var patients = await _patientRepository.GetAllAsync(p => p.Appointments);

            //  DTO - המרה מישות ל
            return patients.Select(p => new PatientResponseDto
            {
                Id = p.Id,
                FullName = $"{p.FirstName} {p.LastName}",
                Email = p.Email,
                PhoneNumber = p.PhoneNumber
            });
        }

        public async Task<PatientResponseDto> GetByIdAsync(int id)
        {
            var p = await _patientRepository.GetByIdAsync(id, p => p.Appointments);
            if (p == null) return null;

            return new PatientResponseDto
            {
                Id = p.Id,
                FullName = $"{p.FirstName} {p.LastName}",
                Email = p.Email,
                PhoneNumber = p.PhoneNumber
            };
        }

        public async Task<PatientResponseDto> AddAsync(PatientRequestDto patientDto)
        {
            if (string.IsNullOrEmpty(patientDto.Email) || !patientDto.Email.Contains("@"))
            {
                throw new Exception("כתובת אימייל לא תקינה");
            }

            var patientEntity = new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Email = patientDto.Email,
                PhoneNumber = patientDto.PhoneNumber
            };

            //  שמירה בריפוזיטורי
            var savedPatient = await _patientRepository.AddAsync(patientEntity);

            // 3. החזרה כ-ResponseDto
            return new PatientResponseDto
            {
                Id = savedPatient.Id,
                FullName = $"{savedPatient.FirstName} {savedPatient.LastName}",
                Email = savedPatient.Email,
                PhoneNumber = savedPatient.PhoneNumber
            };
        }

        public async Task UpdateAsync(int id, PatientRequestDto patientDto)
        {
            // לודא שהמטופל קיים לפני עדכון
            var exists = await _patientRepository.GetByIdAsync(id);
            if (exists == null)
                throw new Exception("Patient not found");

            // עדכון השדות של הישות הקיימת
            exists.FirstName = patientDto.FirstName;
            exists.LastName = patientDto.LastName;
            exists.Email = patientDto.Email;
            exists.PhoneNumber = patientDto.PhoneNumber;

            await _patientRepository.UpdateAsync(id, exists);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _patientRepository.RemoveAsync(id);
        }

       
    }
}
