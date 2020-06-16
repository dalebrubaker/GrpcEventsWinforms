using GrpcEvents;

namespace Server
{
    public static class MessageExtensions
    {
        public static SampleEventArgsMessage ToSampleEventArgsMessage(this SampleEventArgs args)
        {
            var result = new SampleEventArgsMessage
            {
                AccountName = args.AccountName,
                Sender = args.Source
            };
            return result;
        }
    }
}