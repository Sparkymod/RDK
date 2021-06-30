using System;

namespace RDK.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Cyclic : Attribute
    {
        public Cyclic(int time)
        {
            Time = time;
        }

        public int Time
        {
            get;
            set;
        }
    }
}
