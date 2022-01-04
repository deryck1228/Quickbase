namespace Quickbase
{
    public interface IQbFieldValue
    {
        object Value { get; set; }

        void SetFieldValue(object val);
    }
}