namespace Clinic.Core.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Specialty { get; set; }

        public Appointment() { }
        public Appointment(int Id, DateTime StartTime, int PatientId, Patient Patient, int DoctorId, Doctor Doctor)
        {
            this.Id = Id;
            this.StartTime = StartTime;
            this.PatientId = PatientId;
            this.Patient = Patient;
            this.DoctorId = DoctorId;
            this.Doctor = Doctor;
        }
    }
}
