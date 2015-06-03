namespace Errs.WebUi
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Features.Errors;
    using Features.Shared;
    using Infrastructure;

    public class Global : HttpApplication
    {
        private const string Controller = "controller";
        private const string Action = "action";

        protected void Application_Start(object sender, EventArgs e)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());

            BundleTable.EnableOptimizations = true; 
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((HttpApplication)sender).Context;
            var currentController = " ";
            var currentAction = " ";
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            if (currentRouteData != null)
            {
                if (currentRouteData.Values[Controller] != null && !string.IsNullOrEmpty(currentRouteData.Values[Controller].ToString()))
                {
                    currentController = currentRouteData.Values[Controller].ToString();
                }

                if (currentRouteData.Values[Action] != null && !string.IsNullOrEmpty(currentRouteData.Values[Action].ToString()))
                {
                    currentAction = currentRouteData.Values[Action].ToString();
                }
            }

            var ex = Server.GetLastError();
            var controller = new ErrorsController();
            var routeData = new RouteData();
            var action = "Index";

            if (ex is HttpException)
            {
                var httpException = ex as HttpException;

                switch (httpException.GetHttpCode())
                {
                    case 404 :
                        action = "NotFound";
                        break;
                    case 401 :
                    case 403 :
                        action = "NotAuthorized";
                        break;
                    default :
                        action = "Index";
                        break;
                }
            }
            
            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            httpContext.Response.TrySkipIisCustomErrors = true;
            var isAjaxRequest = httpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            routeData.Values["controller"] = "Errors";
            routeData.Values["action"] = action;

            controller.ViewData.Model = new ErrorInfo(ex, currentController, currentAction, isAjaxRequest);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
    
            Response.End();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}