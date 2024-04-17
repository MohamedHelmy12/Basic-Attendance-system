using Microsoft.EntityFrameworkCore;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;
using MVC_Attendance.Repository;

namespace MVC_Attendance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AttDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("con1"));
            });

            builder.Services.AddScoped<ITrackRepository, TrackRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ISuperviseRepository, SuperviseRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();







            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
