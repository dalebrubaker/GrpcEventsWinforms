using System;

namespace Server
{
    public class SampleEventKey : IEquatable<SampleEventKey>
    {
        public string AccountName { get; }
        public string Requester { get; }

        public SampleEventKey(string accountName, string requester)
        {
            AccountName = accountName;
            Requester = requester;
        }

        public bool Equals(SampleEventKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AccountName == other.AccountName && Requester == other.Requester;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SampleEventKey)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AccountName != null ? AccountName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Requester != null ? Requester.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(SampleEventKey left, SampleEventKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SampleEventKey left, SampleEventKey right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{AccountName} from {Requester}";
        }
    }
}