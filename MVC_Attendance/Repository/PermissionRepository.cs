using Microsoft.EntityFrameworkCore;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;
using System.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC_Attendance.Repository
{
    public class PermissionRepository : IRepository.IPermissionRepository
    {
        private readonly AttDbContext db;
        private readonly IInstructorRepository instructorRepository;
        private readonly IStudentRepository studentRepository;
        public PermissionRepository(AttDbContext _db, IInstructorRepository _instructorRepository, IStudentRepository _studentRepository) {
            db = _db;
            instructorRepository = _instructorRepository;
            studentRepository = _studentRepository;
        }

        public void CreatePermission(Permission permission)
        {
            db.Permissions.Add(permission);
            db.SaveChanges();
        }

        public void DeletePermission(int stdId, DateOnly date)
        {
            var per = db.Permissions.FirstOrDefault(p => p.StudentId == stdId && p.date == date);
            db.Permissions.Remove(per);
            db.SaveChanges();
        }

        public Permission GetPermission(int stdId, DateOnly date)
        {
            var per = db.Permissions.FirstOrDefault(p => p.StudentId == stdId && p.date == date);
            return per;
        }

        public List<Permission> GetPendingPermissions()
        {
            DateTime today = DateTime.Now.Date;
            // && DateTime.Compare(today, p.date.ToDateTime(TimeOnly.MinValue)) < 0
            List<Permission> pending = db.Permissions.Include(p => p.Student).Where(p => p.Status == "pending" ).ToList();
            pending = pending.Where(p => DateTime.Compare(today, p.date.ToDateTime(TimeOnly.MinValue)) <= 0 ).ToList();
            return pending;
        }

        public List<Permission> GetSupervisorStudentsPermissions(int supervisorId)
        {
            List<Permission> permissions = GetPendingPermissions();

            Supervise supervisionInfo = instructorRepository.GetSupervisionInfo(supervisorId);
            Console.WriteLine($"{supervisionInfo.IntakeId} {supervisionInfo.TrackId}");

            List<Permission> supervPermissions = new List<Permission>();

            foreach (var permission in permissions)
            {
                var studentIntakeTrack = studentRepository.GetStdIntakeTrack(permission.StudentId);
                if(studentIntakeTrack.IntakeId == supervisionInfo.IntakeId && studentIntakeTrack.TrackId == supervisionInfo.TrackId)
                    supervPermissions.Add(permission);
            }
            return supervPermissions;
        }
    }
}
