using RDK.Core.Extensions;
using System;

namespace RDK.Core.Tools
{
    public static class Utility
    {

    }

    public static class ConsoleUtility
    {
        public static int Total { get; set; } = 1;
        public static int Count { get; set; } = 1;

        /// <summary>
        /// Show a progress bar based on Console.
        /// </summary>
        public static void WriteProgressBar()
        {
            float percent = (float)Count / Total * 100f;
            switch (percent)
            {
                case <= 10:
                    Console.Write("[■         ] {0,1:0}% [{1}/{2}] {3}\r".Red(), percent, Count, Total);
                    break;
                case <= 20:
                    Console.Write("[■■        ] {0,1:0}% [{1}/{2}] {3}\r".Red(), percent, Count, Total);
                    break;
                case <= 30:
                    Console.Write("[■■■       ] {0,1:0}% [{1}/{2}] {3}\r".Orange(), percent, Count, Total);
                    break;
                case <= 40:
                    Console.Write("[■■■■      ] {0,1:0}% [{1}/{2}] {3}\r".Orange(), percent, Count, Total);
                    break;
                case <= 50:
                    Console.Write("[■■■■■     ] {0,1:0}% [{1}/{2}] {3}\r".Orange(), percent, Count, Total);
                    break;
                case <= 60:
                    Console.Write("[■■■■■■    ] {0,1:0}% [{1}/{2}] {3}\r".Yellow(), percent, Count, Total);
                    break;
                case <= 70:
                    Console.Write("[■■■■■■■   ] {0,1:0}% [{1}/{2}] {3}\r".Yellow(), percent, Count, Total);
                    break;
                case <= 80:
                    Console.Write("[■■■■■■■■  ] {0,1:0}% [{1}/{2}] {3}\r".Yellow(), percent, Count, Total);
                    break;
                case <= 90:
                    Console.Write("[■■■■■■■■■ ] {0,1:0}% [{1}/{2}] {3}\r".Green(), percent, Count, Total);
                    break;
                default:
                    Console.Write("[■■■■■■■■■■] {0,1:0}% [{1}/{2}] {3}\r".Green(), percent, Count, Total);
                    break;
            }
        }
    }
}
