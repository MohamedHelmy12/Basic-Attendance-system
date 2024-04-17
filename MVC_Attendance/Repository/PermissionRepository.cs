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
            List<Permission> pending = db.Permissions.Include(p => p.Student).Where(p => p.Status == PermissionStatus.Pending ).ToList();
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
        public void updatePermissionStatus(Permission per, PermissionStatus status)
        {
            var permission = db.Permissions.FirstOrDefault(p => p.StudentId == per.StudentId && p.date == per.date);
            if (permission != null)
            {
                permission.Status = status;
            }
            db.SaveChanges();
        }

        //public void AcceptPermission(Permission permission)
        //{
        //    updatePermissionStatus(permission, "Accepted");
        //}

        //public void RejectPermission(Permission permission)
        //{
        //    updatePermissionStatus(permission, "Rejected");
        //}

        public void UpdatePermission(Permission permission, PermissionStatus status)
        {
            updatePermissionStatus(permission, status);
        }

    }
}
