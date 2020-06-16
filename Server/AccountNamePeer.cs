namespace Server
{
    public class AccountNamePeer
    {
        public string AccountName { get; }
        public string Peer { get; }

        public AccountNamePeer(string accountName, string peer)
        {
            AccountName = accountName;
            Peer = peer;
        }

        public override string ToString()
        {
            return $"{AccountName}@{Peer}";
        }
    }
}