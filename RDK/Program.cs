using System;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;

namespace RDK
{
    public class Program
    {
        public readonly TimeSpan TimerTimeout = TimeSpan.FromMinutes(5);

        public static void Main(string[] args)
        {
            Settings.LoadBasic();
            
            Log.Information("Test");
            Console.ReadKey();
        }
    }
}
