using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGopnik
{
    class Events
    {
        public static Game1 g;
        public static void main()
        {
            Game1.game_state = gs.MAIN;
        }

        public static void help()
        {
            Game1.game_state = gs.HELP;
        }

        public static void game()
        {
            Game1.game_state = gs.GAME;
        }

        public static void exit()
        {
            g.Exit();
        }
    }
}
