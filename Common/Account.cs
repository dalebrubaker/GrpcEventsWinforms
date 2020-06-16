using System;

namespace Common
{
    public class Account
    {
        public string Name { get; }

        public Account(string name)
        {
            Name = name;
        }

        public event EventHandler<SampleEventArgs> SampleEvent;

        public void OnSampleEvent(string accountName, string sender)
        {
            var args = new SampleEventArgs
            {
                AccountName = accountName,
                Sender = sender
            };
            OnSampleEvent(args);
        }

        public void OnSampleEvent(SampleEventArgs args)
        {
            var tmp = SampleEvent;
            tmp?.Invoke(this, args);
        }
    }
}