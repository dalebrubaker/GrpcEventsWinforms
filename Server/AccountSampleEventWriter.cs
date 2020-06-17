using System;
using Common;
using Grpc.Core;
using GrpcEvents;

namespace Server
{
    public class AccountSampleEventWriter : IDisposable
    {
        private readonly SampleEventKey _sampleEventKey;
        private readonly IServerStreamWriter<SampleEventArgsMessage> _responseStream;
        private readonly LogControl _logControl;
        private readonly Action<SampleEventKey> _removeAction;
        private readonly Account _account;

        public AccountSampleEventWriter(SampleEventKey sampleEventKey, IServerStreamWriter<SampleEventArgsMessage> responseStream,
            LogControl logControl, Action<SampleEventKey> removeAction)
        {
            _sampleEventKey = sampleEventKey;
            _responseStream = responseStream;
            _logControl = logControl;
            _removeAction = removeAction;
            _account = Accounts.RequireAccount(_sampleEventKey.AccountName);
            _account.SampleEvent += AccountOnSampleEvent;
        }

        private async void AccountOnSampleEvent(object sender, SampleEventArgs e)
        {
            try
            {
                _logControl.LogMessage($"Sending {e} to requester={_sampleEventKey}");
                var args = e.ToSampleEventArgsMessage();
                await _responseStream.WriteAsync(args);
            }
            catch (Exception ex)
            {
                // Eo graceful recovery when client has disappeared
                _logControl.LogMessage($"Exception: {ex.Message}");
                _removeAction(_sampleEventKey);
                Dispose();
            }
        }

        public void Dispose()
        {
            _account.SampleEvent -= AccountOnSampleEvent;
        }

        public override string ToString()
        {
            return _sampleEventKey.AccountName;
        }
    }
}