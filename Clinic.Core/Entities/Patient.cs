namespace Clinic.Core.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RequiredSpecialty { get; set; }

        public Patient() { }
        public Patient(int Id, string FirstName, string LastName,
            string PhoneNumber, string Email, string RequiredSpecialty)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.RequiredSpecialty = RequiredSpecialty;
        }
        public string GetFullName() {  return $"{this.FirstName} {this.LastName}"; }
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
