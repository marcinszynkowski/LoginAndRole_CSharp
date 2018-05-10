using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models.DB;
using WebApp.Models.ViewModel;

namespace WebApp
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            //string[] roless = new string[2];

            //using (DemoDBEntities1 db = new DemoDBEntities1())
            //{
            //    SqlConnection sqlConnection1 = new SqlConnection("data source=DESKTOP-3M2OGLT;initial catalog=DemoDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot; ");
            //    SqlCommand cmd = new SqlCommand();
            //    SqlDataReader reader;

            //    cmd.CommandText = "SELECT LOOKUPRoleID, RoleName FROM LOOKUPRole";
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Connection = sqlConnection1;

            //    sqlConnection1.Open();

            //    reader = cmd.ExecuteReader();

            //    int i = 0;
            //    try
            //        {
            //            while (reader.Read())
            //            {
            //                roless[i] = reader[1].ToString();
            //                i++;
            //            }
            //        }
            //        finally
            //        {
            //            // Always call Close when done reading.
            //            reader.Close();
            //        }

            //}

            //return roless;

            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            SqlConnection sqlConnection1 = new SqlConnection("data source=DESKTOP-3M2OGLT;initial catalog=DemoDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot; ");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT lp.RoleName FROM SYSUser su RIGHT JOIN SYSUserRole s ON su.SYSUserID = s.SYSUserID RIGHT JOIN LOOKUPRole lp ON s.LOOKUPRoleID = lp.LOOKUPRoleID WHERE su.LoginName = \'" + username + "\'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();
            string[] roless = new String[1];
            int i = 0;
            try
            {
                while (reader.Read())
                {
                    roless[i] = reader[0].ToString();
                    i++;
                }
            }
            finally
            {
                // Always call Close when done reading.
                reader.Close();
            }
            return roless;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }


        public override bool IsUserInRole(string username, string roleName)
        {
            using (DemoDBEntities1 db = new DemoDBEntities1())
            {
                SYSUser SU = db.SYSUsers.Where(o => o.LoginName.ToLower().Equals(username))?.FirstOrDefault();
                if (SU != null)
                {
                    var roles = from q in db.SYSUserRoles
                                join r in db.LOOKUPRoles on q.LOOKUPRoleID equals r.LOOKUPRoleID
                                where r.RoleName.Equals(roleName) && q.SYSUserID.Equals(SU.SYSUserID)
                                select r.RoleName;

                    if (roles != null)
                    {
                        return roles.Any();
                    }
                }

                return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}