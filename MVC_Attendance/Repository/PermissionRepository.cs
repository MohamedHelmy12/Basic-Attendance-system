using MVC_Attendance.Models;
using System.Security;

namespace MVC_Attendance.Repository
{
    public class PermissionRepository : IRepository.IPermissionRepository
    {
        AttDbContext db;
        public PermissionRepository(AttDbContext _db) {
            db = _db;
        }
    }
}
