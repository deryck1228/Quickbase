namespace Quickbase
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbidAttribute : Attribute
    {
        public string Dbid { get; set; }

        public DbidAttribute(string dbid)
        {
            Dbid = dbid;
        }
    }
}