using System;

namespace RDK.Core.Helpers
{
    public static class AttributeHelper
    {
        [AttributeUsage(AttributeTargets.Method)]
        public class Cyclic : Attribute
        {
            public int Time { get; set;}
            public Cyclic(int time) => Time = time;
        }

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
        public class VariableAttribute : Attribute
        {
            ///<summary>
            ///  Sets a value indicating whether this variable can be set when server is running
            ///</summary>
            ///<value><c>true</c> if this variable can be set when server is running; otherwise, <c>false</c>.</value>
            public bool DefinableRunning { get; set; }
            public int Priority { get; set; }
            public VariableAttribute() => Priority = 1;
            public VariableAttribute(bool definableByConfig = false)
            {
                DefinableRunning = definableByConfig;
                Priority = 1;
            }
        }
    }
}
