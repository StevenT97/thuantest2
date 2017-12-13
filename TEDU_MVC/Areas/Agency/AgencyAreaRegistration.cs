using System.Web.Mvc;

namespace TEDU_MVC.Areas.Agency
{
    public class AgencyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Agency";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Agency_default",
                "Agency/{controller}/{action}/{id}",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional }
            );
        }
    }
}