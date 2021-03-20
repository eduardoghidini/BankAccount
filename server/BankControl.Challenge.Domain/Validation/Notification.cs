namespace BankAccount.Warren.Domain.Validation
{
    public class Notification
    {
        public string Key { get; }

        public string Message { get; }

        public string Field { get; set; }

        public Notification(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public Notification(string key, string message, string field)
        {
            Key = key;
            Message = message;
            Field = field;
        }
    }
}
