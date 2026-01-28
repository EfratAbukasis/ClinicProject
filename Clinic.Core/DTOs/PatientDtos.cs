using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.DTOs
{
    public class PatientDtos
    {
        public class PatientRequestDto
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }

            [EmailAddress(ErrorMessage = "פורמט אימייל לא תקין")]
            public string Email { get; set; }

            [Phone(ErrorMessage = "מספר טלפון לא תקין")]
            public string PhoneNumber { get; set; }
        }

        public class PatientResponseDto
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public object Appointments { get; set; }
        }
    }
}
