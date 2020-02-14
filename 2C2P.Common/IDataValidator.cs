namespace _2C2P.Common
{
    public interface IDataValidator
    {
        bool Validate(DataType type, string value);
    }

    public enum DataType
    {
        CurrencyCode,
        TransactionStatus
    };
}