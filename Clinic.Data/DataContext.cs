using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Clinic.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. הגדרות רופאים
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(d => d.LastName).IsRequired().HasMaxLength(50);
                entity.Property(d => d.PhoneNumber).IsRequired().HasMaxLength(15);
                entity.Property(d => d.Specialty).IsRequired().HasMaxLength(50); // הגדלתי מ-15 ל-50

                entity.HasData(
                    new Doctor { Id = 1, FirstName = "Shalom", LastName = "Zikry", PhoneNumber = "0502564532", Specialty = "Dentist" },
                    new Doctor { Id = 2, FirstName = "Yosef", LastName = "Levi", PhoneNumber = "0533564532", Specialty = "Ophthalmologist" }
                );
            });

            // 2. הגדרות מטופלים
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(15);
                entity.HasIndex(p => p.Email).IsUnique();
                entity.Property(p => p.Email).IsRequired().HasMaxLength(100);
                entity.Property(p => p.RequiredSpecialty).HasMaxLength(100);

                entity.HasData(
                    new Patient { Id = 1, FirstName = "Yonatan", LastName = "Abukasis", PhoneNumber = "0521234567", Email = "Yonatan345@gmail.com", RequiredSpecialty = "Dentist" },
                    new Patient { Id = 2, FirstName = "Sara", LastName = "Levi", PhoneNumber = "0549876543", Email = "sara@gmail.com", RequiredSpecialty = "Ophthalmologist" }
                );
            });


            modelBuilder.Entity<Appointment>(entity =>
            {
                // 1. הגדרת מפתח ראשי
                entity.HasKey(a => a.Id);

                // 2. הגדרת זמן התחלה כשדה חובה
                entity.Property(a => a.StartTime).IsRequired();

                // 3. הגדרת הקשר לרופא (Doctor)
                entity.HasOne(a => a.Doctor)               // לתור יש רופא אחד
                      .WithMany(d => d.Appointments)       // לרופא יש הרבה תורים
                      .HasForeignKey(a => a.DoctorId)      // המפתח המקשר
                      .OnDelete(DeleteBehavior.Restrict);  // הגנה: לא מוחקים רופא אם יש לו תורים

                // 4. הגדרת הקשר למטופל (Patient)
                entity.HasOne(a => a.Patient)              // לתור יש מטופל אחד
                      .WithMany(p => p.Appointments)       // למטופל יש הרבה תורים
                      .HasForeignKey(a => a.PatientId)     // המפתח המקשר
                      .OnDelete(DeleteBehavior.Cascade);   // אם מטופל נמחק, התורים שלו נמחקים איתו

                
                entity.HasData(
                    new Appointment { Id = 1, StartTime = new DateTime(2026, 02, 01, 10, 0, 0), PatientId = 1, DoctorId = 1 },
                    new Appointment { Id = 2, StartTime = new DateTime(2026, 02, 01, 11, 0, 0), PatientId = 2, DoctorId = 1 }
                );
            });
        }
    }
    }

