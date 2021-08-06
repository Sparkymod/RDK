using System;
using System.Collections.Generic;

namespace RDK.Core.Timers
{
    public class TimedTimerEntry : IDisposable
    {
        private int interval;
        private bool firstCalled;
        private int delay;

        public DateTime NextTick { get; private set; }
        public DateTime? LastExecute { get; private set; }
        public Action Action { get; private set; }
        public bool Enabled { get; private set; }
        public bool IsDisposed { get; private set; }
        public int Delay
        {
            get => delay;
            set
            {
                NextTick = !firstCalled && Enabled && value != -1 ? (NextTick - TimeSpan.FromMilliseconds(delay) + TimeSpan.FromMilliseconds(value)) : NextTick;
                delay = value;
            }
        }
        public int Interval
        {
            get => interval;
            set
            {
                NextTick = value != -1 ? NextTick - TimeSpan.FromMilliseconds(interval) + TimeSpan.FromMilliseconds(value) : NextTick;
                interval = value;
            }
        }

        public TimedTimerEntry() { }

        public TimedTimerEntry(int delay, int interval, Action action)
        {
            this.delay = delay;
            Interval = interval;
            Action = action;
        }

        public TimedTimerEntry(int interval, Action action) : this(interval, interval, action) { }

        public void Start()
        {
            NextTick = DateTime.Now + TimeSpan.FromMilliseconds(delay);
            Enabled = true;
        }

        public void Stop() => Enabled = false;

        public void Dispose()
        {
            Enabled = false;
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }

        public bool ShouldTrigger() => Enabled && !IsDisposed && DateTime.Now >= NextTick;

        public bool Trigger()
        {
            Action();
            if (Interval > 0)
            {
                Enabled = true;
            }
            else
            {
                NextTick = DateTime.Now + TimeSpan.FromMilliseconds(Interval);
            }

            firstCalled = true;
            LastExecute = DateTime.Now;

            return Enabled || IsDisposed;
        }

        public override string ToString() => string.Format("{0} (Callback = {1}.{2}, Delay = {3})", GetType(), Action.Target, Action.Method, Delay);
    }

    public class TimedTimerComparer : IComparer<TimedTimerEntry>
    {
        public int Compare(TimedTimerEntry x, TimedTimerEntry y) => x.NextTick.CompareTo(y.NextTick);
    }
}
