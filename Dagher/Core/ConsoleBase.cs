using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dagher.Core
{
    public class ConsoleBase
    {
        private static readonly string[] AsciiLogo =
{
            "\n",
            "  ____                 _                  ",
            " |  _ \\   __ _   __ _ | |__    ___  _ __  ",
            " | | | | / _` | / _` || '_ \\  / _ \\| '__| ",
            " | |_| || (_| || (_| || | | ||  __/| |    ",
            " |____/  \\__,_| \\__, ||_| |_| \\___||_|    ",
            "                |___/                     ",
            "\n",
        };
        private static readonly ConsoleColor[] LogoColors =
{
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkGray,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkYellow,
            ConsoleColor.Green,
            ConsoleColor.Red,
            ConsoleColor.White,
        };

        public void SetTitle(string title)
        {
            Console.Title = title;
        }

        public void DrawAsciiLogo()
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = LogoColors.ElementAt(new Random().Next(LogoColors.Length));

            foreach (string line in AsciiLogo)
            {
                int padding = (Console.BufferWidth + line.Length) / 2;
                Console.WriteLine(line.PadLeft(padding));
            }

            Console.ForegroundColor = color;
        }

        protected virtual void Process() { }

        public void Start() => Task.Factory.StartNew(Process);
        
    }
}
