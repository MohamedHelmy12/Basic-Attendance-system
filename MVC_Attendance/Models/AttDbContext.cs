using Microsoft.EntityFrameworkCore;
using System.Drawing;

using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MVC_Attendance.Repository;

namespace MVC_Attendance.Models
{
    public class AttDbContext : DbContext
    {
        public AttDbContext(DbContextOptions<AttDbContext> options) : base(options) { }
        public AttDbContext()
        { }
        private ScheduleRepository scheduleRepository;
        private AttendanceRepository attendanceRepository;
        private StudentRepository studentRepository;
        private InstructorRepository instructorRepository;

		public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<ITIProgram> ITIPrograms { get; set; }
        public virtual DbSet<Intake> Intakes { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<IntakesTracks> IntakesTracks { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<StdIntakeTrack> StdIntakeTrack { get; set; }
        public virtual DbSet<Supervise> Supervises { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().UseTptMappingStrategy();

            modelBuilder.Entity<IntakesTracks>().HasKey(it => new { it.IntakeId, it.TrackId });

            modelBuilder.Entity<StdIntakeTrack>().HasKey(SIT => new { SIT.IntakeId, SIT.TrackId, SIT.StudentId });

            modelBuilder.Entity<Supervise>().HasKey(s => new { s.TrackId, s.IntakeId, s.InstructorId });

            modelBuilder.Entity<Permission>().HasKey(p => new { p.StudentId, p.date });

            // try to add some seedings 
            // seeding the ITIProgram 
            modelBuilder.Entity<ITIProgram>().HasData(
                new ITIProgram { Id = 1, Name = "Professional Training Program" },
                new ITIProgram { Id = 2, Name = "Intensive Training Program" },
                new ITIProgram { Id = 3, Name = "Summer Training Program" }
                );

            // seeding the Intakes
            modelBuilder.Entity<Intake>().HasData(
                new Intake { Id = 1, Name = "44", ProgramId = 1 }
                );

            // seeding the Tracks
            modelBuilder.Entity<Track>().HasData(
           new Track { Id = 1, Name = "Professional Web Development and BI" },
                new Track { Id = 2, Name = "Open Source" },
                new Track { Id = 3, Name = "Artificial Intelegence" }
                );

            // seeding the IntakesTracks
            modelBuilder.Entity<IntakesTracks>().HasData(
                new IntakesTracks { IntakeId = 1, TrackId = 1 }
                );

            // seeding the Users, Students, Instructors, Employees
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Admin", LastName = "Admin", Email = "Admin@admin.com", Password = "Admin@123", Phone = "01111111111", Role = Role.Admin }); // User (Admin)

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 2, FirstName = "Ali", LastName = "Ali2", Email = "Ali@gmail.com", Password = "Ali@123", Phone = "01111111111", Role = Role.Student,
                UniversityID = "Kafr El Shi5",Faculty = "Engineering",Specialization = "Computer Science",GraduationYear = 2023,Address = "Kafr El Shi5, Egypt"}, // Student
                new Student { Id = 3, FirstName = "Ahmed", LastName = "Ahmed2", Email = "Ahmed@gmail.com", Password = "Ahmed@123", Phone = "01111111111", Role = Role.Student,
                UniversityID = "Tanta",Faculty = "Engineering",Specialization = "Mechancial Engineering",GraduationYear = 2023,Address = "Tanta, Egypt"}); // Student 

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { Id = 4, FirstName = "Nadya", LastName = "Saleh", Email = "Nadya@gmail.com", Password = "Nadya@123", Phone = "01111111111", Role = Role.Instructor }, // Instructor Supervisor
                new Instructor { Id = 5, FirstName = "Ayman", LastName = "Lotfy", Email = "Ayman@gmail.com", Password = "Ayman@123", Phone = "01111111111", Role = Role.Instructor }); // Instructor

            modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 6, FirstName = "Mahmoud", LastName = "Mahmoud", Email = "Mahmoud@gmail.com", Password = "Mahmoud@123", Phone = "01111111111", Role = Role.Employee,
                EmployeeType = EmployeeType.StudentAffairs,Salary = 10000,HireDate = new DateTime(2000, 1, 1)}, // Employee Student Affairs
                new Employee { Id = 7, FirstName = "Ashraf", LastName = "Ashraf2", Email = "Ashraf@gmail.com", Password = "admin@123", Phone = "01111111111", Role = Role.Employee,
                EmployeeType = EmployeeType.Security,Salary = 10000,HireDate = new DateTime(2001, 1, 1) // Employee Security
                });

            // 1 - User (Admin) // Email: Admin@admin, Password: Admin@123
            // 2 - Student  // Email: Ali@gmail, Password: Ali@123
            // 3 - Stduent  // Email: Ahmed@gmail, Password: Ahmed@123
            // 4 - Instructor Supervisor // Email: Nadya@gmail, Password: Nadya@123
            // 5 - Instructor // Email: Ayman@gmail, Password: Ayman@123
            // 6 - Employee Student Affairs // Email: Mahmoud@gmail, Password: Mahmoud@123
            // 7 - Employee Security // Email: Ashraf@gmail, Password: admin@123

            // seeding the Schedules // Id, StartPeriod, TrackId
            // create list if schedules for trackId = 1;

            for (int i = 1; i <= 28; i++)
            {
                int counter = 2 * (i - 1);
                var schedule = new Schedule { Id = i, StartPeriod = new TimeOnly(9, 0), Date = new DateOnly(2024, 4, i), TrackId = 1 };
                modelBuilder.Entity<Schedule>().HasData(schedule);

                var att1 = new Attendance { Id = counter + 1, UserId = 2, ScheduleId = i};
                var att2 = new Attendance { Id = counter + 2, UserId = 3, ScheduleId = i };

                modelBuilder.Entity<Attendance>().HasData(att1, att2);

            }

            //modelBuilder.Entity<Schedule>().HasData(
            //    new Schedule { Id = 1, StartPeriod = new TimeOnly(9, 0),Date = new DateOnly(2024,4,20), TrackId = 1 },
            //    new Schedule { Id = 2, StartPeriod = new TimeOnly(9, 0), Date = new DateOnly(2024, 4, 21), TrackId = 1 },
            //    new Schedule { Id = 3, StartPeriod = new TimeOnly(9, 0), Date = new DateOnly(2024, 4, 22), TrackId = 1 },
            //    new Schedule { Id = 4, StartPeriod = new TimeOnly(9, 0), Date = new DateOnly(2024, 4, 23), TrackId = 1 },
            //    new Schedule { Id = 5, StartPeriod = new TimeOnly(9, 0), Date = new DateOnly(2024, 4, 24), TrackId = 1 },
            //    new Schedule { Id = 6, StartPeriod = new TimeOnly(9, 0), Date = new DateOnly(2024, 4, 25), TrackId = 1 },
            //    new Schedule { Id = 7, StartPeriod = new TimeOnly(9, 0), Date = new DateOnly(2024, 4, 27), TrackId = 1 }

            //    );

            // seeding the StdIntakeTrack // StudentId, IntakeId, TrackId
            modelBuilder.Entity<StdIntakeTrack>().HasData(
                new StdIntakeTrack { StudentId = 2, IntakeId = 1, TrackId = 1 },
                new StdIntakeTrack { StudentId = 3, IntakeId = 1, TrackId = 1 }
                );

            // seeding the Supervise // TrackId, IntakeId, InstructorId
            modelBuilder.Entity<Supervise>().HasData(
                new Supervise { TrackId = 1, IntakeId = 1, InstructorId = 5 }
                );

            // seeding the Permissions // StudentId, date

            // 
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=LAPTOP-4UUJ0C6J\\MSSQLSERVER02;Database=Attendance;integrated security = true; trust server certificate = true");

        }
    }

}       
