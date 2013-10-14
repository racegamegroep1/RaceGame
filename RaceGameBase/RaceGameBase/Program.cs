using System;

namespace RaceGameBase
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        static void Main(string[] args)
        {
            Settings.Initialize();

            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
#endif
}

