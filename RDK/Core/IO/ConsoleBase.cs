using System;
using System.Linq;
using System.Threading.Tasks;

namespace RDK.Core.IO
{
    public class ConsoleBase
    {
        public string[] Logo { get; set; }

        private readonly ConsoleColor[] LogoColors =
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

        public ConsoleBase() { }

        public virtual void SetTitle(string title)
        {
            Console.Title = title;
        }

        public void DrawAsciiLogo()
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = LogoColors.ElementAt(new Random().Next(LogoColors.Length));

            foreach (string line in Logo)
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
