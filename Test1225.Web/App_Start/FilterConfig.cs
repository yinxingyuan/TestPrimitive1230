using System.Web.Mvc;
using SSO.Utilities;


namespace Test1225.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeBaseAttribute());
        }
    }
}