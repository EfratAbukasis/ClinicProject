using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Clinic.Core.DTOs.PatientDtos;


namespace Clinic.Core.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientResponseDto>> GetAllAsync();
        Task<PatientResponseDto> GetByIdAsync(int id);
        Task<PatientResponseDto> AddAsync(PatientRequestDto patientDto);
        Task UpdateAsync(int id, PatientRequestDto patientDto);
        Task<bool> DeleteAsync(int id);
    }
}
