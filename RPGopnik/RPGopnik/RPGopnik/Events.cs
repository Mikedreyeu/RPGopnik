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

        public static void AddSpell_AddHP()
        {
            SLScreen.unlockedAt100xp_content.Remove(ContentLoader.sl_menu_content.add_hp);
            SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.add_hp_l);
            g.game.character.inventory.Learn(Spell.Type.AddHp);
        }

        public static void AddSpell_Heal()
        {
            SLScreen.unlockedAt100xp_content.Remove(ContentLoader.sl_menu_content.heal);
            SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.heal_l);
            g.game.character.inventory.Learn(Spell.Type.Heal);
        }

        public static void AddSpell_Move()
        {
            SLScreen.unlockedAt100xp_content.Remove(ContentLoader.sl_menu_content.move);
            SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.move_l);
            g.game.character.inventory.Learn(Spell.Type.Move);
        }

        public static void AddSpell_Revive()
        {
            SLScreen.unlockedAt200xp_content.Remove(ContentLoader.sl_menu_content.revive);
            SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.revive_l);
            g.game.character.inventory.Learn(Spell.Type.Revieve);
        }

        public static void AddSpell_Antidote()
        {
            SLScreen.unlockedAt100xp_content.Remove(ContentLoader.sl_menu_content.antidote);
            SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.antidote_l);
            g.game.character.inventory.Learn(Spell.Type.Antidote);
        }

        public static void AddSpell_Shield()
        {
            SLScreen.unlockedAt200xp_content.Remove(ContentLoader.sl_menu_content.shield);
            SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.shield_l);
            g.game.character.inventory.Learn(Spell.Type.Shield);
        }
    }
}
