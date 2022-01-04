namespace Quickbase
{
    public interface IQbField
    {
        string FieldId { get; set; }
        IQbFieldValue Value { get; set; }
    }
}