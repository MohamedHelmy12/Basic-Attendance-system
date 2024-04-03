using Microsoft.EntityFrameworkCore;

namespace MVC_Attendance.Models
{
    public class AttDbContext : DbContext
    {
        public AttDbContext(DbContextOptions<AttDbContext> options) : base(options) { }

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

            modelBuilder.Entity<IntakesTracks>().HasKey(it => new {it.IntakeId, it.TrackId});

            modelBuilder.Entity<StdIntakeTrack>().HasKey(SIT => new {SIT.IntakeId, SIT.TrackId, SIT.StudentId});

            modelBuilder.Entity<Supervise>().HasKey(s => new { s.TrackId, s.IntakeId, s.InstructorId });

            modelBuilder.Entity<Permission>().HasKey(p => new {p.StudentId, p.date});

            base.OnModelCreating(modelBuilder);
        }
    }
}
