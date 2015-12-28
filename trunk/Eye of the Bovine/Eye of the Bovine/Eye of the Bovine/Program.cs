using System;

namespace Eye_of_the_Bovine
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (App game = App.GetInstance())
            {
                game.Run();
            }
        }
    }
#endif
}

