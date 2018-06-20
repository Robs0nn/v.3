using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace SnacesOficina_v2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
