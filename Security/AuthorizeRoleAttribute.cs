﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models.DB;
using WebApp.Models.EntityManager;

namespace WebApp.Security
{
    public class AuthorizeRoleAttribute
    {
        public class AuthorizeRolesAttribute : AuthorizeAttribute
        {
            private readonly string[] userAssignedRoles;
            public AuthorizeRolesAttribute(params string[] roles)
            {
                this.userAssignedRoles = roles;
            }
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                bool authorize = false;
                using (DemoDBEntities1 db = new DemoDBEntities1())
                {
                    UserManager UM = new UserManager();
                    foreach (var roles in userAssignedRoles)
                    {
                        authorize = UM.IsUserInRole(httpContext.User.Identity.Name, roles);
                        if (authorize)
                            return authorize;
                    }
                }
                return authorize;
            }
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                filterContext.Result = new RedirectResult("~/Home/UnAuthorized");
            }
        }
    }
}