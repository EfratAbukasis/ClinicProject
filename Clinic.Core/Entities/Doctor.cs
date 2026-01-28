namespace Clinic.Core.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialty { get; set; }
        public decimal Salary { get; set; }

        public Doctor() { }
        public Doctor(int Id, string FirstName, string LastName, string LicenseNumber, string PhoneNumber, string Specialty, decimal Salary)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.LicenseNumber = LicenseNumber;
            this.PhoneNumber = PhoneNumber;
            this.Specialty = Specialty;
            this.Salary = Salary;
        }

        public string GetFullName() { return $"{this.FirstName} {this.LastName}"; }
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
    

