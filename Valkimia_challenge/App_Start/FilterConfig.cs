using System.Web;
using System.Web.Mvc;
using Valkimia_App.Filters;

namespace Valkimia_challenge
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new VerificacionLogin());
        }
    }
}
