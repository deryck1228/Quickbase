namespace Quickbase
{
    public interface IQbContext
    {
        string Realm { get; init; }
        string Usertoken { get; init; }
    }
}