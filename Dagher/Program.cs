using System;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;

namespace Dagher
{
    public class Program
    {
        public readonly TimeSpan TimerTimeout = TimeSpan.FromMinutes(5);

        public static void Main(string[] args)
        {
            Settings.LoadBasicSettings();
            
            Log.Information("Test");
            Console.ReadKey();
        }
    }

    public partial class UserStorage
    {
        private readonly DbContextOptions options;
    }
}
