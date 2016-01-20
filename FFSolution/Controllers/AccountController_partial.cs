using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using FFSolution.Models;
using FFSolution.Commons;

namespace FFSolution.Controllers
{
    [Authorize]
    public partial class AccountController : Controller
    {
        public ActionResult GetUserCredintionals()
        {
            using (FFSecurityDBEntities ff = new FFSecurityDBEntities())
            {
                var user = ff.AspNetUsers.FirstOrDefault(s => s.UserName == HttpContext.User.Identity.Name);
                if (user != null)
                {
                    var uJson = new
                    {
                        UserName = user.UserName,
                        IsAdministrator = user.IsAdministrator,
                        IsCustomerService = user.IsCustomerService,
                        IsOperation = user.IsOperation,
                        IsSales = user.IsSales,
                        IsAccounting = user.IsAccounting,
                        IsCustomerClearance = user.IsCustomClearance,
                    };

                    return Json(uJson, JsonRequestBehavior.AllowGet);
                }
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}