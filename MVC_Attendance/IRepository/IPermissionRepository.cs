using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IPermissionRepository
    {
        void CreatePermission(Permission permission);
        void DeletePermission(int stdId, DateOnly date);
        Permission GetPermission(int stdId, DateOnly date);
        List<Permission> GetPendingPermissions();
        List<Permission> GetSupervisorStudentsPermissions(int supervisorId);
        
    }
}
