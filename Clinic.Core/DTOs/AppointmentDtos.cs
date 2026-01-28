using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.DTOs
{
    public class AppointmentRequestDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        public object Specialty { get; set; }
    }

    public class AppointmentResponseDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        // מידע ידידותי למשתמש במקום רק מספרים
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string Specialty { get; set; }
    }
}
