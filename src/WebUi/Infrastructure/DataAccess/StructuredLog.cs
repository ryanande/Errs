namespace Errs.WebUi.Infrastructure.DataAccess
{
    using Massive;

    public class StructuredLog : DynamicModel
    {
        //private static readonly ConnectionStrings ConnectionStrings = new ConnectionStrings();
        private static readonly Configurations Configurations = new Configurations();

        public StructuredLog()
            : base("LoggingDb", Configurations.LogTableName, "id")
        {
        }
    }
}