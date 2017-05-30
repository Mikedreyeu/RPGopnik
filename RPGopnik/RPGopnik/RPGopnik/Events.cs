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

        public static void story1()
        {
            Game1.game_state = gs.STORY1;
        }

        public static void story2()
        {
            Game1.game_state = gs.STORY2;
        }

        public static void game()
        {
            Game1.game_state = gs.GAME;
        }

        public static void exit()
        {
            g.Exit();
        }

        public static void Use_BigHP()
        {
            g.game.character.inventory.Use(Artefact.Type.BigHP);
        }

        public static void Use_MedHP()
        {
            g.game.character.inventory.Use(Artefact.Type.MedHP);
        }

        public static void Use_SmaHP()
        {
            g.game.character.inventory.Use(Artefact.Type.SmaHP);
        }

        public static void Use_BigMP()
        {
            g.game.character.inventory.Use(Artefact.Type.BigMP);
        }

        public static void Use_MedMP()
        {
            g.game.character.inventory.Use(Artefact.Type.MedMP);
        }

        public static void Use_SmaMP()
        {
            g.game.character.inventory.Use(Artefact.Type.SmaMP);
        }

        public static void Use_Rose()
        {
            g.game.character.inventory.Use(Artefact.Type.Rose);
        }

        public static void Use_Colesa()
        {
            g.game.character.inventory.Use(Artefact.Type.Colesa);
        }

        public static void Use_Balanda()
        {
            g.game.character.inventory.Use(Artefact.Type.Balanda);
        }

        public static void Use_PlayBoy()
        {
            g.game.character.inventory.Use(Artefact.Type.PlayBoy);
        }

        public static void AddSpell_AddHP()
        {
            if (g.game.character.inventory.spells.addhp == null)
            {
                SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.add_hp_l);
                g.game.character.inventory.Learn(Spell.Type.AddHp);
            }
        }

        public static void AddSpell_Heal()
        {
            if (g.game.character.inventory.spells.heal == null)
            {
                SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.heal_l);
                g.game.character.inventory.Learn(Spell.Type.Heal);
            }
        }

        public static void AddSpell_Move()
        {
            if (g.game.character.inventory.spells.move == null)
            {
                SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.move_l);
                g.game.character.inventory.Learn(Spell.Type.Move);
            }
        }

        public static void AddSpell_Revive()
        {
            if (g.game.character.inventory.spells.revieve == null)
            {
                SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.revive_l);
                g.game.character.inventory.Learn(Spell.Type.Revieve);
            }
        }

        public static void AddSpell_Antidote()
        {
            if (g.game.character.inventory.spells.antidote == null)
            {
                SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.antidote_l);
                g.game.character.inventory.Learn(Spell.Type.Antidote);
            }
        }

        public static void AddSpell_Shield()
        {
            if (g.game.character.inventory.spells.shield == null)
            {
                SLScreen.unlockedIcons_content.Add(ContentLoader.sl_menu_content.shield_l);
                g.game.character.inventory.Learn(Spell.Type.Shield);
            }
        }
    }
}
