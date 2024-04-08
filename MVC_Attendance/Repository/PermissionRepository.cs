using Microsoft.EntityFrameworkCore;
using MVC_Attendance.Models;
using System.Security;

namespace MVC_Attendance.Repository
{
    public class PermissionRepository : IRepository.IPermissionRepository
    {
        private readonly AttDbContext db;
        public PermissionRepository(AttDbContext _db) {
            db = _db;
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

            return pending;
        }
    }
}
