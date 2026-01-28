using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponseDto>> GetAllAsync();
        Task<DoctorResponseDto> GetByIdAsync(int id);
        Task<DoctorResponseDto> AddAsync(DoctorRequestDto doctorDto);
        Task UpdateAsync(int id, DoctorRequestDto doctorDto);
        Task<bool> DeleteAsync(int id);

    }
}
