﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebApp.Security.AuthorizeRoleAttribute;
using WebApp.Models.ViewModel;
using WebApp.Models.EntityManager;
using System.Web.Routing;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Welcome()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminOnly()
        {
            return View();
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageUserPartial(string status = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                string loginName = User.Identity.Name;
                UserManager UM = new UserManager();
                UserDataView UDV = UM.GetUserDataView(loginName);

                string message = string.Empty;
                if (status.Equals("update"))
                    message = "Update Successful";
                else if (status.Equals("delete"))
                    message = "Delete Successful";

                ViewBag.Message = message;

                return PartialView(UDV);
            }

            return RedirectToAction("Welcome", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UpdateUserData(int userID, string loginName, string password, string firstName, string lastName, string gender, int roleID = 0)
        {
            UserProfileView UPV = new UserProfileView();
            UPV.SYSUserID = userID;
            UPV.LoginName = loginName;
            UPV.Password = password;
            UPV.FirstName = firstName;
            UPV.LastName = lastName;
            UPV.Gender = gender;

            if (roleID > 0)
                UPV.LOOKUPRoleID = roleID;

            UserManager UM = new UserManager();
            UM.UpdateUserAccount(UPV);

            return Json(new { success = true });
        }

        [Authorize]
        public ActionResult DeleteUser(int userID)
        {
            UserManager UM = new UserManager();
            UM.DeleteUser(userID);
            return Json(new { JsonRequestBehavior.AllowGet });
        }

        [Authorize]
        public ActionResult EditProfile(int userId)
        {
            string loginName = User.Identity.Name;
            UserManager UM = new UserManager();
            UserProfileView UPV = UM.GetUserProfile(userId);
            return View(UPV);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditProfile(UserProfileView profile)
        {
            if (ModelState.IsValid)
            {
                UserManager UM = new UserManager();
                UM.UpdateUserAccount(profile);

                ViewBag.Status = "Update Sucessful!";
            }

            return View(profile);
        }
    }
}