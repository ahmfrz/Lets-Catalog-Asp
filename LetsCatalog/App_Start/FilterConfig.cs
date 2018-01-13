using LetsCatalog.Filters;
using StructureMap;
using System.Web;
using System.Web.Mvc;

namespace LetsCatalog
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoggingFilter());
        }
    }
}
