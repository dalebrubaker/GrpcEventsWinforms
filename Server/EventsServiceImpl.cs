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

        private readonly ConcurrentDictionary<AccountNamePeer, AccountSampleEventWriter> _eventWriters = new ConcurrentDictionary<AccountNamePeer, AccountSampleEventWriter>();

        public EventsServiceImpl(LogControl logControl)
        {
            _logControl = logControl;
        }

        public override async Task Subscribe(IAsyncStreamReader<SubscribeRequest> requestStream, IServerStreamWriter<SampleEventArgsMessage> responseStream, ServerCallContext context)
        {
            var requesterHeader = context.RequestHeaders.FirstOrDefault(x => x.Key == Constants.Requester.ToLower()); // Must be lower case. Ugh!
            var peer = context.Peer;
            var requester = $"{requesterHeader}@{peer}";
            while (await requestStream.MoveNext())
            {
                RemoveDisposedWriters();
                var request = requestStream.Current;
                var key = new AccountNamePeer(request.AccountName, peer);
                if (request.StartStop)
                {
                    var writer = new AccountSampleEventWriter(request.AccountName, responseStream, requester, _logControl);
                    _eventWriters.TryAdd(key, writer);
                    OnSampleEvent(request.AccountName);
                }
                else
                {
                    _eventWriters.TryRemove(key, out _);
                    return;
                }
            }
        }

        private void RemoveDisposedWriters()
        {
            var keys = _eventWriters.Keys.ToList();
            foreach (var key in keys)
            {
                var value = _eventWriters[key];
                if (value.IsDisposed)
                {
                    _eventWriters.TryRemove(key, out _);
                }
            }
        }

        public event EventHandler<string> AccountNameAdded;

        public void OnSampleEvent(string accountName)
        {
            var tmp = AccountNameAdded;
            tmp?.Invoke(this, accountName);
        }
    }
}