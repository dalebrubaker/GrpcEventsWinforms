using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;

namespace Common
{
    /// <summary>
    /// This class only reduces code a bit by hiding the check "if (_syncContext != SynchronizationContext.Current)"
    /// </summary>
    public class SynchronizationContextHelper : IEquatable<SynchronizationContextHelper>
    {
        private readonly SynchronizationContext _syncContext;

        public SynchronizationContextHelper()
        {
            _syncContext = SynchronizationContext.Current;
            //s_logger.Debug("Setting _syncContext");
        }

        /// <summary>
        /// Use SynchronizationContext.Send() if on the current SynchronizationContext
        /// Use Send if you need to get something done as soon as possible.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="state"></param>
        public void Send(SendOrPostCallback d, object state = null)
        {
            try
            {
                if (_syncContext != SynchronizationContext.Current)
                {
                    _syncContext.Send(d, state);
                }
                else
                {
                    d?.Invoke(state);
                }
            }
            catch (InvalidAsynchronousStateException)
            {
                // Can happen on shutdown.
                // System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. --->
                // System.ComponentModel.InvalidAsynchronousStateException: An error occurred invoking the method.
                // The destination thread no longer exists.
            }
            catch (TargetInvocationException)
            {
                // Can happen on shutdown.
                // System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. --->
                // System.ComponentModel.InvalidAsynchronousStateException: An error occurred invoking the method.
                // The destination thread no longer exists.
            }
        }

        /// <summary>
        /// Use SynchronizationContext.Post() if on the current SynchronizationContext
        /// Use Post to wait our turn in the queue.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="state"></param>
        public void Post(SendOrPostCallback d, object state = null)
        {
            if (_syncContext != SynchronizationContext.Current)
            {
                _syncContext.Post(d, state);
            }
            else
            {
                d?.Invoke(state);
            }
        }

        public bool Equals(SynchronizationContextHelper other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_syncContext, other._syncContext);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SynchronizationContextHelper)obj);
        }

        public override int GetHashCode()
        {
            return _syncContext != null ? _syncContext.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return _syncContext.ToString();
        }
    }
}