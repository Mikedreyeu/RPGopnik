using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class ContentLoader
    {
        public struct Game_Content
        {
            public Enemy enemy;
            public Character character;
            public Texture2D beer;
            public Texture2D boyarishnik;
            public Texture2D rose;
            public Texture2D colesa;
            public Texture2D balanda;
            public Texture2D playBoy;
        }

        public struct Game_GUI_Content
        {
            public Content foundation;
            public SpriteFont hp_mana_font;
            public Texture2D add_hp;
            public Texture2D heal;
            public Texture2D antidote;
            public Texture2D revive;
            public Texture2D shield;
            public Texture2D move;
        }

        public struct Menu_Content
        {
            public Content background;
            public Content logo;
        }

        public struct Main_Menu_Content
        {
            public Button game_button;
            public Button help_button;
            public Button exit_button;
        }

        public struct Help_Menu_Content
        {
            public Content info;
            public Button main_menu;
        }

        public struct Pause_Content
        {
            public Content background;
            public Content pause_header;
        }

        public struct Pause_Menu_Content
        {
            public Button resume_button;
            public Button exit_button;
        }

        public struct SL_Menu_Content
        {
            public Button resume_button;
            public Content spells_window;
            public Button add_hp;
            public Button heal;
            public Button antidote;
            public Button revive;
            public Button shield;
            public Button move;
            public Content add_hp_l;
            public Content heal_l;
            public Content antidote_l;
            public Content revive_l;
            public Content shield_l;
            public Content move_l;
        }

        public static Menu_Content menu_content;
        public static Help_Menu_Content help_menu_content;
        public static Main_Menu_Content main_menu_content;
        public static Pause_Content pause_content;
        public static Pause_Menu_Content pause_menu_content;
        public static SL_Menu_Content sl_menu_content;
        public static Game_GUI_Content game_gui_content;
        public static Game_Content game_content;

        public static void Load(ContentManager Content, Viewport viewp)
        {
            game_gui_content.hp_mana_font = Content.Load<SpriteFont>("bt_font");

            help_menu_content.info = new Content(new Rectangle(viewp.Width / 2 - 350, viewp.Height - 400, 700, 250), Content.Load<Texture2D>("help"));
            help_menu_content.main_menu = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height - 100, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.main), "Main menu");
            menu_content.logo = new Content(new Rectangle(viewp.Width / 2 - 437, viewp.Height / 15, 874, 250), Content.Load<Texture2D>(@"GUI\logo"));
            menu_content.background = new Content(new Rectangle(0, 0, viewp.Width, viewp.Height), Content.Load<Texture2D>("title_screen_background"));
            main_menu_content.game_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height / 2, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.game), "New Game");
            main_menu_content.help_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height / 2 + 70, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.help), "Help");
            main_menu_content.exit_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height / 2 + 140, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.exit), "Exit");
            pause_content.background = new Content(new Rectangle(0, 0, viewp.Width, viewp.Height), Content.Load<Texture2D>(@"GUI\pause_background_opacity"));
            pause_content.pause_header = new Content(new Rectangle(viewp.Width / 2 - 253, viewp.Height / 15, 507, 150), Content.Load<Texture2D>(@"GUI\pause"));
            pause_menu_content.resume_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height / 2 + 70, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.game), "Resume");
            pause_menu_content.exit_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height / 2 + 140, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.main), "Main Menu");
            game_gui_content.foundation = new Content(new Rectangle(0, 0, viewp.Width, viewp.Height), Content.Load<Texture2D>(@"GUI\foundation"));
            game_gui_content.add_hp = Content.Load<Texture2D>(@"SpellButtons\sp_heal");
            game_gui_content.antidote = Content.Load<Texture2D>(@"SpellButtons\sp_antidote");
            game_gui_content.heal = Content.Load<Texture2D>(@"SpellButtons\sp_cure");
            game_gui_content.move = Content.Load<Texture2D>(@"SpellButtons\sp_unfreeze");
            game_gui_content.revive = Content.Load<Texture2D>(@"SpellButtons\sp_resurrection");
            game_gui_content.shield = Content.Load<Texture2D>(@"SpellButtons\sp_armor");
            sl_menu_content.spells_window = new Content(new Rectangle(viewp.Width / 2 - 170, viewp.Height / 2 - 234, 340, 549), Content.Load<Texture2D>(@"GUI\spell_learning"));
            sl_menu_content.resume_button = new Button(new Rectangle(viewp.Width / 2 + 139, viewp.Height / 2 - 211, 20, 20), Content.Load<Texture2D>(@"GUI\btExit"), Content.Load<Texture2D>(@"GUI\btExit"), Content.Load<Texture2D>(@"GUI\btExit"), new Event(Events.game), "");
            sl_menu_content.add_hp = new Button(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 - 75, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_heal"), Content.Load<Texture2D>(@"SpellButtons\sp_heal"), Content.Load<Texture2D>(@"SpellButtons\sp_heal_p"), new Event(Events.AddSpell_AddHP), "");
            sl_menu_content.heal = new Button(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 - 10, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_cure"), Content.Load<Texture2D>(@"SpellButtons\sp_cure"), Content.Load<Texture2D>(@"SpellButtons\sp_cure_p"), new Event(Events.AddSpell_Heal), "");
            sl_menu_content.antidote = new Button(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 + 55, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_antidote"), Content.Load<Texture2D>(@"SpellButtons\sp_antidote"), Content.Load<Texture2D>(@"SpellButtons\sp_antidote_p"), new Event(Events.AddSpell_Antidote), "");
            sl_menu_content.move = new Button(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 + 120, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_unfreeze"), Content.Load<Texture2D>(@"SpellButtons\sp_unfreeze"), Content.Load<Texture2D>(@"SpellButtons\sp_unfreeze_p"), new Event(Events.AddSpell_Move), "");
            sl_menu_content.revive = new Button(new Rectangle(viewp.Width / 2 - 57, viewp.Height / 2 - 75, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_resurrection"), Content.Load<Texture2D>(@"SpellButtons\sp_resurrection"), Content.Load<Texture2D>(@"SpellButtons\sp_resurrection_p"), new Event(Events.AddSpell_Revive), "");
            sl_menu_content.shield = new Button(new Rectangle(viewp.Width / 2 - 57, viewp.Height / 2 - 10, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_armor"), Content.Load<Texture2D>(@"SpellButtons\sp_armor"), Content.Load<Texture2D>(@"SpellButtons\sp_armor_p"), new Event(Events.AddSpell_Shield), "");
            sl_menu_content.add_hp_l = new Content(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 - 75, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_heal_l"));
            sl_menu_content.heal_l = new Content(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 - 10, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_cure_l"));
            sl_menu_content.antidote_l = new Content(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 + 55, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_antidote_l"));
            sl_menu_content.move_l = new Content(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 + 120, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_unfreeze_l"));
            sl_menu_content.revive_l = new Content(new Rectangle(viewp.Width / 2 - 57, viewp.Height / 2 - 75, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_resurrection_l"));
            sl_menu_content.shield_l = new Content(new Rectangle(viewp.Width / 2 - 57, viewp.Height / 2 - 10, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_armor_l"));
            game_content.enemy = new Enemy(new Vector2(800, 800), 1, new Animation(100, Content.Load<Texture2D>("enemy"), 30, 32, 3));
            game_content.character = new Character("pesos", Races.Baryga, "Male", new Animation(100, Content.Load<Texture2D>("gopnik_texture"), 30, 32, 3), new Vector2(300, 300), 2);
            game_content.beer = Content.Load<Texture2D>("beer");
            game_content.boyarishnik = Content.Load<Texture2D>("boyarishnik");
            game_content.rose = Content.Load<Texture2D>("rose");
            game_content.balanda = Content.Load<Texture2D>("balanda");
            game_content.colesa = Content.Load<Texture2D>("colesa");
            game_content.playBoy = Content.Load<Texture2D>("playboy");
        }
    }
}