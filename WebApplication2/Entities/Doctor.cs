namespace ClinicAPI.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialty { get; set; }

        public Doctor(int Id, string FirstName, string LastName, string PhoneNumber, string Specialty)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.Specialty = Specialty;
        }

        public string GetFullName() { return $"{this.FirstName} {this.LastName}"; }
    }
}
    

