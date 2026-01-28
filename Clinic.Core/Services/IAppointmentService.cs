using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentResponseDto>> GetAllAsync();
        Task<AppointmentResponseDto> GetByIdAsync(int id);
        Task<AppointmentResponseDto> AddAsync(AppointmentRequestDto appointmentDto);
        Task UpdateAsync(int id, AppointmentRequestDto appointmentDto);
        Task<bool> DeleteAsync(int id);

    }
}
