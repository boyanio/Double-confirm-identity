using System.Web.Mvc;

namespace DoubleConfirmIdentity.Examples.FormsAuth
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
