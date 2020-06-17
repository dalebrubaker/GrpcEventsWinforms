using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Grpc.Core;
using GrpcEvents;

namespace Client
{
    public class Subscription : IDisposable, IEquatable<Subscription>
    {
        private readonly string _clientName;
        private readonly LogControl _logControl;
        private readonly Metadata _headers;
        private readonly Action<string> _removeSubscription;
        private AsyncDuplexStreamingCall<SubscribeRequest, SampleEventArgsMessage> _call;

        public string AccountName { get; }

        public Subscription(string accountName, string clientName, LogControl logControl, Metadata headers, Action<string> removeSubscription)
        {
            _clientName = clientName;
            _logControl = logControl;
            _headers = headers;
            _removeSubscription = removeSubscription;
            AccountName = accountName;
        }

        /// <summary>
        /// Connect to server. Return <c>false</c> if subscribe fails
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SubscribeAsync()
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
            while (_call == null)
            {
                await Task.Delay(10).ConfigureAwait(false);
            }
            try
            {
                await _call.RequestStream.WriteAsync(request);
            }
            catch (RpcException ex)
            {
                _logControl.LogMessage($"{ex.Message}\nRemember to use the StartServer button on FormServer");
                _call = null;
                _removeSubscription(AccountName);
                return false;
            }
            return true;
        }

        private async Task StartReceivingEvents()
        {
            try
            {
                var channel = new Channel("localhost:" + Constants.ServerPort, ChannelCredentials.Insecure);
                var client = new EventsService.EventsServiceClient(channel);
                using (_call = client.Subscribe(_headers))
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
                _removeSubscription(AccountName);
                throw;
            }
        }

        public async Task UnsubscribeAsync()
        {
            var request = new SubscribeRequest
            {
                StartStop = false,
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
                Dispose();
                _removeSubscription(AccountName);
            }
        }

        public void Dispose()
        {
            _call?.Dispose();
            _call = null;
        }

        public override string ToString()
        {
            return AccountName;
        }

        public bool Equals(Subscription other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AccountName == other.AccountName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Subscription)obj);
        }

        public override int GetHashCode()
        {
            return AccountName != null ? AccountName.GetHashCode() : 0;
        }

        public static bool operator ==(Subscription left, Subscription right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Subscription left, Subscription right)
        {
            return !Equals(left, right);
        }
    }
}