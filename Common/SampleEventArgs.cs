namespace Common
{
    public class SampleEventArgs
    {
        public string AccountName { get; set; }
        public string Sender { get; set; }

        public override string ToString()
        {
            return $"AccountName={AccountName} from {Sender}";
        }
    }
}