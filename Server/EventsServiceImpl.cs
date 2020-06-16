using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Grpc.Core;
using GrpcEvents;

namespace Server
{
    public class EventsServiceImpl : EventsService.EventsServiceBase
    {
        private readonly LogControl _logControl;
        
        private ConcurrentDictionary<string, AccountSampleEventWriter> _eventWriters = new ConcurrentDictionary<string, AccountSampleEventWriter>();

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
                var key = $"{request.AccountName}@{peer}";
                if (request.StartStop)
                {
                    writer = new AccountSampleEventWriter(request.AccountName, responseStream, _logControl);
                    _eventWriters.TryAdd(key, writer);
                }
                else
                {
                    _eventWriters.TryRemove(key, out _);
                    return;
                }
            }        
        }
    }
}