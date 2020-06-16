using GrpcEvents;

namespace Common
{
    public static class MessageExtensions
    {
        public static SampleEventArgsMessage ToSampleEventArgsMessage(this SampleEventArgs args)
        {
            var result = new SampleEventArgsMessage
            {
                AccountName = args.AccountName,
                Sender = args.Sender
            };
            return result;
        }
        
        public static SampleEventArgs ToSampleEventArgs(this SampleEventArgsMessage args)
        {
            var result = new SampleEventArgs
            {
                AccountName = args.AccountName,
                Sender = args.Sender
            };
            return result;
        }
    }
}