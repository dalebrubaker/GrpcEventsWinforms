using System;

namespace Server
{
    public class Account
    {
        public string Name { get; }

        public Account(string name)
        {
            Name = name;
        }

        public event EventHandler<SampleEventArgs> OrderUpdate;

        public void OnSampleEvent(string accountName, string source)
        {
            var args = new SampleEventArgs
            {
                AccountName = accountName,
                Source = source
            };
            var tmp = OrderUpdate;
            tmp?.Invoke(this, args);
        }
    }
}