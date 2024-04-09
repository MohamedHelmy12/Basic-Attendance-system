using Microsoft.AspNetCore.Authentication.Cookies;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;
using MVC_Attendance.ViewModels;
using System.Security.Claims;

namespace MVC_Attendance.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AttDbContext db;
        public AccountRepository(AttDbContext _db)
        { 
            db = _db;
        }

        public ClaimsPrincipal AddUserAuthentication(User userLogin)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            Claim userEmail = new Claim(ClaimTypes.Email, userLogin.Email);

            Claim Role = new Claim(ClaimTypes.Role, userLogin.Role.ToString());
            claimsIdentity.AddClaim(userEmail);
            claimsIdentity.AddClaim(Role);
            claimsPrincipal.AddIdentity(claimsIdentity);
            return claimsPrincipal;
        }

        public User GetUserAuth(UserLoginModelView userLogin)
        {
            return db.Users.FirstOrDefault(u => u.Email == userLogin.Email && u.Password == userLogin.Password);
        }
        public void AddUser(UserRegisterModelView userRegister)
        {
            if(userRegister.Role == Role.Student)
            {
                db.Students.Add(new Student()
                {
                    FirstName = userRegister.Fname,
                    LastName = userRegister.Lname,
                    Email = userRegister.Email,
                    Password = userRegister.Password,
                    Role = userRegister.Role,
                    UniversityID = userRegister.UniversityID,
                    Faculty = userRegister.Faculty,
                    Specialization = userRegister.Specialization,
                    GraduationYear = userRegister.GraduationYear
                });
            }
            else if(userRegister.Role == Role.Employee)
            {
                db.Employees.Add(new Employee()
                {
                    FirstName = userRegister.Fname,
                    LastName = userRegister.Lname,
                    Email = userRegister.Email,
                    Password = userRegister.Password,
                    Role = userRegister.Role,
                    EmployeeType = userRegister.EmployeeType,
                    HireDate = DateTime.Now,
                });
            }

            else if (userRegister.Role == Role.Instructor)
            {
                db.Instructors.Add(new Instructor()
                {
                    FirstName = userRegister.Fname,
                    LastName = userRegister.Lname,
                    Email = userRegister.Email,
                    Password = userRegister.Password,
                    Role = userRegister.Role,
                });
            }

            db.SaveChanges();

        }

        public User? GetUserByEmail(string Email)
        {
            User u = db.Users.FirstOrDefault(u => u.Email == Email);

            if (u == null)
                return null;
            return u;
        }


    }
}
