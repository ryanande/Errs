namespace Errs.WebUi.Features.Shared
{
    using System;
    using System.Web.Mvc;

    public class ErrorInfo : HandleErrorInfo
    {
        public ErrorInfo(Exception exception, string controllerName, string actionName)
            : base(exception, controllerName, actionName)
        {
        }

        public ErrorInfo(Exception exception, string controllerName, string actionName, bool isAjaxRequest)
            : base(exception, controllerName, actionName)
        {
            IsAjaxRequest = isAjaxRequest;
        }

        public bool IsAjaxRequest { get; set; }
    }
}