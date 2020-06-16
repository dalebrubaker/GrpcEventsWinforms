namespace Client
{
    public class Subscription
    {
        public string AccountName { get; }

        public Subscription(string accountName)
        {
            AccountName = accountName;
        }

        public override string ToString()
        {
            return AccountName;
        }
    }
}