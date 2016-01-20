using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FFSolution.Models;

namespace FFSolution.Controllers
{
    public partial class AgentController : Controller
    {
        //zaatar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateByNameOnly(string aname, int acountry)
        {
            Agent a = null;
            try
            {
                a = new Agent()
                {
                    AgentName = aname,
                    CountryID = acountry
                };
                db.Agent.Add(a);
                db.SaveChanges();
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
    }
}
