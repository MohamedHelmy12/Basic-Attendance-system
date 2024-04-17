using MVC_Attendance.Models;
using MVC_Attendance.ViewModels;
using System.Security.Claims;

namespace MVC_Attendance.IRepository
{
    public interface IAccountRepository
    {
        User GetUserAuth(UserLoginModelView userLogin);
        ClaimsPrincipal AddUserAuthentication(User userLogin);
        User? GetUserByEmail(string Email);
        void AddUser(UserRegisterModelView userRegister);
    }
}
