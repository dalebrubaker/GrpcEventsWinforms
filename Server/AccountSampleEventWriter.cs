using System;
using Common;
using Grpc.Core;
using GrpcEvents;

namespace Server
{
    public class AccountSampleEventWriter : IDisposable
    {
        private readonly IServerStreamWriter<SampleEventArgsMessage> _responseStream;
        private readonly string _requester;
        private readonly LogControl _logControl;
        private readonly Account _account;
        
        public string AccountName { get; }
        
        public bool IsDisposed { get; private set; }


        public AccountSampleEventWriter(string accountName, IServerStreamWriter<SampleEventArgsMessage> responseStream, string requester, LogControl logControl)
        {
            _responseStream = responseStream;
            _requester = requester;
            _logControl = logControl;
            AccountName = accountName;
            _account = Accounts.RequireAccount(accountName);
            _account.SampleEvent += AccountOnSampleEvent;
        }

        private async void AccountOnSampleEvent(object sender, SampleEventArgs e)
        {
            try
            {
                _logControl.LogMessage($"Sending {e} to requester={_requester}");
                var args = e.ToSampleEventArgsMessage();
                await _responseStream.WriteAsync(args);
            }
            catch (Exception ex)
            {
                _logControl.LogMessage($"Exception: {ex.Message}");
                
                // Dispose instead of throwing for graceful recovery when client has disappeared
                Dispose();
            }
        }

        public void Dispose()
        {
            _account.SampleEvent -= AccountOnSampleEvent;
            IsDisposed = true;
        }

        public override string ToString()
        {
            return AccountName;
        }
    }
}