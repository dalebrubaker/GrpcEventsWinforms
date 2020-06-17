using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Grpc.Core;
using GrpcEvents;

namespace Server
{
    public class EventsServiceImpl : EventsService.EventsServiceBase
    {
        private readonly LogControl _logControl;

        private readonly ConcurrentDictionary<SampleEventKey, AccountSampleEventWriter> _eventWriters = new ConcurrentDictionary<SampleEventKey, AccountSampleEventWriter>();

        public EventsServiceImpl(LogControl logControl)
        {
            _logControl = logControl;
        }

        public override async Task Subscribe(IAsyncStreamReader<SubscribeRequest> requestStream, IServerStreamWriter<SampleEventArgsMessage> responseStream, ServerCallContext context)
        {
            var requesterHeader = context.RequestHeaders.FirstOrDefault(x => x.Key == Constants.Requester.ToLower()); // Must be lower case. Ugh!
            var peer = context.Peer;
            var requester = requesterHeader == null ? $"Anonymous@{peer}" : $"{requesterHeader.Value}@{peer}";
            while (await requestStream.MoveNext())
            {
                var request = requestStream.Current;
                var key = new SampleEventKey(request.AccountName, requester);
                if (request.StartStop)
                {
                    var writer = new AccountSampleEventWriter(key, responseStream, _logControl, RemoveWriter);
                    _eventWriters.TryAdd(key, writer);
                    OnAccountNameAdded(request.AccountName);
                    _logControl.LogMessage($"Added subscription to SampleEvents for {AccountNameAdded}");
                }
                else
                {
                    RemoveWriter(key);
                    return;
                }
            }
        }

        private void RemoveWriter(SampleEventKey key)
        {
            _eventWriters.TryRemove(key, out _);
            OnAccountNameRemoved(key.AccountName);
            _logControl.LogMessage($"Removed subscription to SampleEvents for {AccountNameAdded}");
        }

        public event EventHandler<string> AccountNameAdded;

        public void OnAccountNameAdded(string accountName)
        {
            var tmp = AccountNameAdded;
            tmp?.Invoke(this, accountName);
        }
        
        public event EventHandler<string> AccountNameRemoved;

        public void OnAccountNameRemoved(string accountName)
        {
            var tmp = AccountNameRemoved;
            tmp?.Invoke(this, accountName);
        }
    }
}