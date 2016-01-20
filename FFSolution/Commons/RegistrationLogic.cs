using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFSolution.Commons
{
    public class RegistrationLogic
    {
        public bool IsAdministratorPasswordValid(string password)
        {
            using (FFSolution.Models.FFAdminDBEntities db = new Models.FFAdminDBEntities())
            {
                if(db.Config.FirstOrDefault().SystemPassword == password)
                    return true;
                return false;
            }
        }

        public void SetCanLogin(string username)
        {
            using (FFSolution.Models.FFSecurityDBEntities db = new Models.FFSecurityDBEntities())
            {
                var u = db.AspNetUsers.FirstOrDefault(x => x.UserName == username);
                if (u != null)
                    u.CanLogin = true;

            }
        }


        public bool CanLogin(string username)
        {
            using (FFSolution.Models.FFSecurityDBEntities db = new Models.FFSecurityDBEntities())
            {
                var u = db.AspNetUsers.FirstOrDefault(x => x.UserName == username);
                if (u == null)
                    return false;
                else
                {
                    var l = true;
                    try
                    {
                        l = u.CanLogin;
                    }
                    catch
                    {}
                        return l;
                }
            }
        }


        public void SavePlainPassword(string username, string password)
        {
            using (FFSolution.Models.FFSecurityDBEntities db = new Models.FFSecurityDBEntities())
            {
                var u = db.AspNetUsers.FirstOrDefault(x => x.UserName == username);
                if (u == null)
                    return;
                else
                {
                    u.PlainPassword = password;
                    db.SaveChanges();
                }
            }
        }
    }
}