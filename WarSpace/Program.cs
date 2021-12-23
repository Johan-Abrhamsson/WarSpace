using System;


namespace WarSpace
{
    static class Program
    {
        public static GameStart startingGame = new GameStart();
        static void Main(string[] args)
        {
            startingGame.Start();
        }
    }
}