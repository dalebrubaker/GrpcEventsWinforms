using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
            var peer = context.Peer;
            while (await requestStream.MoveNext())
            {
                var request = requestStream.Current;
                AccountSampleEventWriter writer;
                var key = new AccountNamePeer(request.AccountName, peer);
                if (request.StartStop)
                {
                    writer = new AccountSampleEventWriter(request.AccountName, responseStream, _logControl);
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
        
        public event EventHandler<string> AccountNameAdded;

        public void OnSampleEvent(string accountName)
        {
            var tmp = AccountNameAdded;
            tmp?.Invoke(this, accountName);
        }
    }
}