namespace Errs.WebUi.Features.Home
{
    using System;

    public class ErrorsResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public string Exception { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Properties { get; set; }
    }
}