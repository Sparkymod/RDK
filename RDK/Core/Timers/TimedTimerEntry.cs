using System;
using System.Collections.Generic;

namespace RDK.Core.Timers
{
    public class TimedTimerEntry : IDisposable
    {
        private int m_interval;
        private bool m_firstCalled;
        private int m_delay;

        public DateTime NextTick { get; private set; }
        public DateTime? LastExecute { get; private set; }
        public Action Action { get; private set; }
        public bool Enabled { get; private set; }
        public bool IsDisposed { get; private set; }
        public int Delay
        {
            get => m_delay;
            set
            {
                NextTick = !m_firstCalled && Enabled && value != -1 ? (NextTick - TimeSpan.FromMilliseconds(m_delay) + TimeSpan.FromMilliseconds(value)) : NextTick;
                m_delay = value;
            }
        }
        public int Interval
        {
            get => m_interval;
            set
            {
                NextTick = value != -1 ? NextTick - TimeSpan.FromMilliseconds(m_interval) + TimeSpan.FromMilliseconds(value) : NextTick;
                m_interval = value;
            }
        }

        public TimedTimerEntry() { }

        public TimedTimerEntry(int delay, int interval, Action action)
        {
            m_delay = delay;
            Interval = interval;
            Action = action;
        }

        public TimedTimerEntry(int interval, Action action) : this(interval, interval, action) { }

        public void Start()
        {
            NextTick = DateTime.Now + TimeSpan.FromMilliseconds(m_delay);
            Enabled = true;
        }

        public void Stop() => Enabled = false;

        public void Dispose()
        {
            Enabled = false;
            IsDisposed = true;
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

            m_firstCalled = true;
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
