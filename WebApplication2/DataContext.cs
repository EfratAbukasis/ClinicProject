using ClinicAPI.Entities;

namespace ClinicAPI
{
    public class DataContext : IDataContext
    {
        public List<Appointment> appointments{ get; set;}
        public List<Doctor> doctors { get; set; }
        public List<Patient> patients { get; set; }

        public DataContext() {
            appointments = new List<Appointment> {new Appointment
            (1, 2, 2, DateTime.Now), new Appointment(2, 1, 1, DateTime.Now)};

            doctors = new List<Doctor> {new Doctor(1,
            "Shalom", "Zikri", "0525234789", "Dentist"), new Doctor(2,
            "Yosef", "Chaim", "0532014814", "Pediatrician")};

            patients = new List<Patient>{ new Patient(1, "Sara",
            "Levi", "0556742398", "Ophthalmologist"),
            new Patient(2, "Chaim", "Cohen", "0556772011", "Dentist")};

        }


    }
}
