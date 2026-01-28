using Clinic.Core.Repositories;
using Clinic.Core.Services;
using ClinicAPI;
using Clinic.Data;
using ClinicAPI; 
using System.Text.Json.Serialization;
using Clinic.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. הגדרות בסיסיות וטיפול במעגליות של JSON (חשוב מאוד לקשרים בין ישויות)
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Clinic.Data.DataContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Clinic_DB;Trusted_Connection=True;"));

// 3. הזרקת תלויות (DI) - החלפתי את הסטודנטים ברופאים ומטופלים של הקליניקה
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//builder.Services.AddScoped<IDoctorService, DoctorService>();
//builder.Services.AddScoped<IPatientService, PatientService>();
//builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(typeof(MappingProfile)));

var app = builder.Build();

// 5. הגדרות Pipeline (איך הבקשה עוברת בשרת)
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseShabbat();

app.MapControllers();

app.Run();