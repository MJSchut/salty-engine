using System;
using salty.game;

namespace salty.desktopgl
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using var game = new GameState();

            game.Run();
        }
    }
}