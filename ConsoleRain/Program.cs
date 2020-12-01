using System;

namespace ConsoleRain
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLayer c = new ConsoleLayer();
            while(true)
            {
                c.Update();
                Console.Clear();
                Console.Write(c.Read());
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
