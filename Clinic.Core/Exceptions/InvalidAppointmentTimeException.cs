using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Exceptions
{
    public class InvalidAppointmentTimeException : Exception
    {
       
            public InvalidAppointmentTimeException(DateTime attemptedTime)
                : base($"לא אפשרי לקבוע תור לזמן שעבר: {attemptedTime:dd/MM/yyyy HH:mm}. נא לבחור זמן עתידי.")
            { }
        

    }
}
