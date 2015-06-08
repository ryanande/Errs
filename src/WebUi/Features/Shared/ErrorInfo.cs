namespace Errs.WebUi.Features.Shared
{
    using System;
    using System.Web.Mvc;

    public class ErrorInfo : HandleErrorInfo
    {
        public ErrorInfo(Exception exception, string controllerName, string actionName, int statusCode)
            : base(exception, controllerName, actionName)
        {
            StatusCode = statusCode;
        }

        public ErrorInfo(Exception exception, string controllerName, string actionName, int statusCode, bool isAjaxRequest)
            : this(exception, controllerName, actionName, statusCode)
        {
            IsAjaxRequest = isAjaxRequest;
        }

        public bool IsAjaxRequest { get; set; }
        
        public int StatusCode { get; set; }
    }
}