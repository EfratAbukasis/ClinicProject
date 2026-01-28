using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.DTOs
{

    // מה שהמשתמש שולח
    public class DoctorRequestDto
    {
        [Required(ErrorMessage = "שם פרטי הוא שדה חובה")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "שם משפחה הוא שדה חובה")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public string LicenseNumber { get; set; }

        public string Specialization { get; set; }
    }

    // מה שהמשתמש מקבל
    public class DoctorResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } // FirstName + LastName
        public string LicenseNumber { get; set; }
        public string Specialization { get; set; }
        public object Appointments { get; set; }
    }
}
