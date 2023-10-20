using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VehicleRentalSystem.Context;
using VehicleRentalSystem.Helper;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly VehicleRentalContext _dbContext = new VehicleRentalContext();
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Vehicles");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var matchedUser = _dbContext.Users
               .FirstOrDefault(u => u.Email.ToLower() == user
               .Email.ToLower() && user
               .Password == user.Password);

                if (matchedUser != null && matchedUser.Email != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    return RedirectToAction("Index", "Vehicles");
                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User registerUser)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = registerUser.Email,
                    Name = registerUser.Name,
                    Password = registerUser.Password,
                    IsActive = true,
                    IsLockedOut = false,
                    CreatedAt = DateTime.Now
                };

                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();
                
                UserRoleMapping userRoleMapping = new UserRoleMapping()
                {
                    RoleId = (int)UserRole.User,
                    UserId = newUser.Id
                };

                _dbContext.UserRoleMappings.Add(userRoleMapping);
                _dbContext.SaveChanges();
                return RedirectToAction("Login");

            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}