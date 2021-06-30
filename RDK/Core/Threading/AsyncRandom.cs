using System;
using System.Threading;

namespace RDK.Core.Threading
{
    /// <summary>
    ///   Represent a Random class that generate a thread unique seed
    /// </summary>
    public sealed class AsyncRandom : Random
    {
        private static int Incrementer;

        public AsyncRandom(int seed) : base(seed) { }

        public AsyncRandom() : base(Environment.TickCount + Thread.CurrentThread.ManagedThreadId + Incrementer)
        {
            unchecked
            {
                Interlocked.Increment(ref Incrementer);
            }
        }

        public double NextDouble(double min, double max)
        {
            return NextDouble() * (max - min) + min;
        }
    }
}
