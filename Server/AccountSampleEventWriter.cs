using System;
using Common;
using Grpc.Core;
using GrpcEvents;

namespace Server
{
    public class AccountSampleEventWriter : IDisposable
    {
        private readonly IServerStreamWriter<SampleEventArgsMessage> _responseStream;
        private readonly LogControl _logControl;
        private readonly Account _account;
        public string AccountName { get; }

        public AccountSampleEventWriter(string accountName, IServerStreamWriter<SampleEventArgsMessage> responseStream, LogControl logControl)
        {
            _responseStream = responseStream;
            _logControl = logControl;
            AccountName = accountName;
            _account = Accounts.RequireAccount(accountName);
            _account.SampleEvent += AccountOnSampleEvent;
        }

        private async void AccountOnSampleEvent(object sender, SampleEventArgs e)
        {
            try
            {
                _logControl.LogMessage($"Sending {e}");
                var args = e.ToSampleEventArgsMessage();
                await _responseStream.WriteAsync(args);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public void Dispose()
        {
            _account.SampleEvent -= AccountOnSampleEvent;
        }

        public override string ToString()
        {
            return AccountName;
        }
    }
}