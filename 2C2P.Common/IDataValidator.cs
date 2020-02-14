namespace _2C2P.Common
{
    public interface IDataValidator
    {
        /// <summary>
        /// Validate given string with corresponding data type.
        /// This will throw exception if validation failed
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="FormatException" />
        bool Validate(DataType type, string value);
    }

    public enum DataType
    {
        TransactionId,
        CurrencyCode,
        TransactionStatus
    };
}