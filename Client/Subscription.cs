using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Grpc.Core;
using GrpcEvents;

namespace Client
{
    public class Subscription : IDisposable
    {
        private readonly string _clientName;
        private readonly LogControl _logControl;
        private readonly EventsService.EventsServiceClient _client;
        private AsyncDuplexStreamingCall<SubscribeRequest, SampleEventArgsMessage> _call;

        public string AccountName { get; }

        public Subscription(string accountName, string clientName, LogControl logControl)
        {
            _clientName = clientName;
            _logControl = logControl;
            AccountName = accountName;
            var channel = new Channel("localhost:" + Constants.ServerPort, ChannelCredentials.Insecure);
            _client = new EventsService.EventsServiceClient(channel);
        }

        public async Task SubscribeAsync()
        {
            if (_call == null)
            {
                _ = Task.Run(StartReceivingEvents);
            }
            var request = new SubscribeRequest
            {
                StartStop = true,
                AccountName = AccountName
            };
            _logControl.LogMessage($"{_clientName} is subscribing to event {request.AccountName}");
            try
            {
                await _call.RequestStream.WriteAsync(request);
            }
            catch (RpcException ex)
            {
                _logControl.LogMessage(ex.Message);
                _call = null;
                throw;
            }
        }

        private async Task StartReceivingEvents()
        {
            try
            {
                using (_call = _client.Subscribe())
                {
                    _logControl.LogMessage($"{_clientName} is waiting for events...");
                    while (await _call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var sampleEventArgsMessage = _call.ResponseStream.Current;
                        var sampleEventArgs = sampleEventArgsMessage.ToSampleEventArgs();
                        var msg = $"Received {sampleEventArgs}";
                        _logControl.LogMessage(msg);
                        var account = new Account(sampleEventArgs.AccountName);
                        account.OnSampleEvent(sampleEventArgs);
                        msg = $"Re-threw event client-side {sampleEventArgs}";
                        _logControl.LogMessage(msg);
                    }
                }
            }
            catch (RpcException ex)
            {
                _logControl.LogMessage(ex.Message);
                _call = null;
                throw;
            }
        }

        public async Task UnsubscribeAsync()
        {
            var request = new SubscribeRequest
            {
                StartStop = true,
                AccountName = AccountName
            };
            try
            {
                _logControl.LogMessage($"{_clientName} is un-subscribing from event {request.AccountName}");
                await _call.RequestStream.WriteAsync(request);
            }
            catch (RpcException ex)
            {
                _logControl.LogMessage(ex.Message);
                _call = null;
                throw;
            }
        }

        public override string ToString()
        {
            return AccountName;
        }

        public void Dispose()
        {
            _call?.Dispose();
            _call = null;
        }
    }
}