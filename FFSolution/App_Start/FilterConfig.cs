using System.Web;
using System.Web.Mvc;

namespace FFSolution
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //added to authroize all actions and controllers
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
        }
    }
}
