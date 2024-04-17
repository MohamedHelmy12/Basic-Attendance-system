using Microsoft.EntityFrameworkCore;
using MVC_Attendance.IRepository;
using MVC_Attendance.Repository;
using MVC_Attendance.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

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
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped < IPermissionRepository, PermissionRepository >();
            builder.Services.AddScoped < IStudentRepository, StudentRepository >();
            builder.Services.AddScoped < IInstructorRepository, InstructorRepository >();
            builder.Services.AddScoped < IAccountRepository, AccountRepository >();
            builder.Services.AddScoped < ITrackRepository, TrackRepository >();
            builder.Services.AddScoped < IScheduleRepository, ScheduleRepository >();
            builder.Services.AddScoped < IAttendanceRepository, AttendanceRepository >();
            builder.Services.AddScoped < IEmployeeRepository, EmployeeRepository >();
            builder.Services.AddScoped< ISuperviseRepository, SuperviseRepository >();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                // pattern: "{controller=Home}/{action=Index}/{id?}");
                pattern: "{controller=Account}/{action=Login}");

            app.Run();
        }
    }
}
