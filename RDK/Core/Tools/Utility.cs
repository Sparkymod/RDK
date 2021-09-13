using RDK.Core.Extensions;

namespace RDK.Core.Tools
{
    public static class Utility
    {
        public static void ConsoleProgressBar(float percent)
        {
            ClearCurrentConsoleLine();
            Console.Write(" [");
            for (int i = 0; i < 10; i++)
            {
                if (i >= percent / 10)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write('■');
                }
            }

            if (percent <= 25f)
            {
                Console.Write("]");
                Console.Write(" {0,1:0}%\r".Red(), percent);
            }
            else if (percent <= 50f)
            {
                Console.Write("]");
                Console.Write(" {0,1:0}%\r".Yellow(), percent);
            }
            else if (percent <= 75f)
            {
                Console.Write("]");
                Console.Write(" {0,1:0}%\r".Green(), percent);
            }

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
