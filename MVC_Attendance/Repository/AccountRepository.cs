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
        private readonly IInstructorRepository instructorRepository;
        public AccountRepository(AttDbContext _db, IInstructorRepository _instructorRepository)
        { 
            db = _db;
            instructorRepository = _instructorRepository;
        }

        public ClaimsPrincipal AddUserAuthentication(User userLogin)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            Claim userEmail = new Claim(ClaimTypes.Email, userLogin.Email);
            Claim userName = new Claim(ClaimTypes.Name, userLogin.FirstName + " " + userLogin.LastName);
            if(userLogin.Role == Models.Role.Instructor)
            {
                Claim isSupervisor;
                Claim customClaim;
                
                if (instructorRepository.GetSupervisionInfo(userLogin.Id) ==  null)
                {
                     isSupervisor = new Claim(ClaimTypes.Actor, "notSupervisor");
                     customClaim = new Claim("supervisor", "no");
                }
                else
                {
                     isSupervisor = new Claim(ClaimTypes.Actor, "supervisor");
                     customClaim = new Claim("supervisor", "yes");
                }
                claimsIdentity.AddClaim(isSupervisor);
                claimsIdentity.AddClaim(customClaim);

            }

            Claim Role = new Claim(ClaimTypes.Role, userLogin.Role.ToString());
            claimsIdentity.AddClaim(userEmail);
            claimsIdentity.AddClaim(Role);
            claimsIdentity.AddClaim(userName);
            claimsPrincipal.AddIdentity(claimsIdentity);
            return claimsPrincipal;
        }

        public User GetUserAuth(UserLoginModelView userLogin)
        {
            // adding try and catch 
            try
            {
                var mydbUsers = db.Users.ToList();
                return db.Users.FirstOrDefault(u => u.Email == userLogin.Email && u.Password == userLogin.Password);
            } catch
            {
                return null;
            }

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
