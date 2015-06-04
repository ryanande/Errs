namespace Errs.WebUi
{
    using System.Web.Mvc;
    using Infrastructure.ExceptionManagement;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionHandler());
        }
    }
}