using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFSolution.Commons
{
    public class SalesManLogic
    {

        public static bool IsSales(string username)
        {
            using (FFSolution.Models.FFSecurityDBEntities db = new Models.FFSecurityDBEntities())
            {
                var user = db.AspNetUsers.Where(x => x.UserName == username);
                if (user.Count() != 0)
                {
                    if (user.FirstOrDefault().IsSales)
                        return true;
                }
                return false;
            }
        }

        public static int GetSalesManID(string username)
        {
            // if username is found , get sales man details
            using (FFSolution.Models.FFAdminDBEntities dbAdmin = new Models.FFAdminDBEntities())
            {
                var salesman = dbAdmin.SalesMan.Where(x => x.UserName == username);
                if (salesman.Count() != 0)
                {
                    return salesman.FirstOrDefault().SalesManID;
                }
            }
            return -1;

        }
    

        public static bool CheckSalesManLogin(string username, int? salesmanId)
        {
            if (salesmanId.HasValue)
            {
                var realSalesMan = GetSalesManID(username);

                if (realSalesMan == salesmanId)
                    return true;
            }
            return false;

        }
    }
}