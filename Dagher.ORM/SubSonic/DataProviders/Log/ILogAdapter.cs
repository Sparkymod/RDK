using System;

namespace Dagher.ORM.SubSonic.DataProviders.Log
{
    public interface ILogAdapter
    {
        void Log(String message);
    }
}