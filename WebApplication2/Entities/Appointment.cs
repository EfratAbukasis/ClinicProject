namespace ClinicAPI.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartTime { get; set; }

        public Appointment(int Id, int PatientId, int DoctorId, DateTime StartTime)
        {
            this.Id = Id;
            this.PatientId = PatientId;
            this.DoctorId = DoctorId;
            this.StartTime = StartTime;
        }
    }
}
