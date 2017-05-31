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
        public struct Cursor_States
        {
            public Texture2D Normal;
            public Texture2D Choosing;
        }

        public struct Button_Content
        {
            public Texture2D infoBox;
        }

        public struct Game_Content
        {
            public Enemy enemy;
            public Enemy baryga;
            public Enemy kolshik;
            public Enemy petuh;
            public Character character;
            public Texture2D fight_txtr;
            public Texture2D dead_enemy_txtr;
            public Texture2D beer;
            public Texture2D boyarishnik;
            public Texture2D rose;
            public Texture2D colesa;
            public Texture2D balanda;
            public Texture2D playBoy;
            public SpriteFont PowerFont;
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
            public Button BigHP;
            public Button MedHP;
            public Button SmaHP;
            public Button BigMP;
            public Button MedMP;
            public Button SmaMP;
            public Button Rose;
            public Button Colesa;
            public Button PlayBoy;
            public Button Balanda;
        }

        public struct Menu_Content
        {
            public Content background;
            public Content logo;
        }
        public struct Story_Content
        {
            public Button continue_button;
            public Button game_button;
            public Content story1;
            public Content story2;
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

        public static Cursor_States cursor_states;
        public static Menu_Content menu_content;
        public static Help_Menu_Content help_menu_content;
        public static Main_Menu_Content main_menu_content;
        public static Pause_Content pause_content;
        public static Pause_Menu_Content pause_menu_content;
        public static SL_Menu_Content sl_menu_content;
        public static Game_GUI_Content game_gui_content;
        public static Game_Content game_content;
        public static Button_Content button_content;
        public static Story_Content story_content;

        public static void Load(ContentManager Content, Viewport viewp)
        {
            game_gui_content.hp_mana_font = Content.Load<SpriteFont>("bt_font");
            game_content.PowerFont = Content.Load<SpriteFont>("PowerFont");

            help_menu_content.info = new Content(new Rectangle(viewp.Width / 2 - 520, viewp.Height - 450, 1000, 350), Content.Load<Texture2D>("help"));
            help_menu_content.main_menu = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height - 100, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.main), "Main menu");
            menu_content.logo = new Content(new Rectangle(viewp.Width / 2 - 437, viewp.Height / 15, 874, 250), Content.Load<Texture2D>(@"GUI\logo"));
            menu_content.background = new Content(new Rectangle(0, 0, viewp.Width, viewp.Height), Content.Load<Texture2D>("title_screen_background"));
            main_menu_content.game_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height / 2, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.story1), "New Game");
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
            story_content.story1 = new Content(new Rectangle(0, 0, viewp.Width, viewp.Height), Content.Load<Texture2D>("Story1"));
            story_content.story2 = new Content(new Rectangle(0, 0, viewp.Width, viewp.Height), Content.Load<Texture2D>("Story2"));
            story_content.continue_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height - 100, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.story2), "Continue");
            story_content.game_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height - 100, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.game), "Continue");
            sl_menu_content.spells_window = new Content(new Rectangle(viewp.Width / 2 - 170, viewp.Height / 2 - 234, 340, 549), Content.Load<Texture2D>(@"GUI\spell_learning"));
            sl_menu_content.resume_button = new Button(new Rectangle(viewp.Width / 2 + 139, viewp.Height / 2 - 211, 20, 20), Content.Load<Texture2D>(@"GUI\btExit"), Content.Load<Texture2D>(@"GUI\btExit"), Content.Load<Texture2D>(@"GUI\btExit"), new Event(Events.game), "");
            sl_menu_content.add_hp = new Button(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 - 75, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_heal"), Content.Load<Texture2D>(@"SpellButtons\sp_heal"), Content.Load<Texture2D>(@"SpellButtons\sp_heal_p"), new Event(Events.AddSpell_AddHP), "", "Добавить здоровье", "Увеличивает текущее значение HP какого-либо персонажа. Cost: 1HP - 2MP");
            sl_menu_content.heal = new Button(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 - 10, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_cure"), Content.Load<Texture2D>(@"SpellButtons\sp_cure"), Content.Load<Texture2D>(@"SpellButtons\sp_cure_p"), new Event(Events.AddSpell_Heal), "", "Вылечить", "Переводит какого-либо персонажа из состояния \"болен\" в состояние \"здоров или ослаблен\". Cost: 20MP");
            sl_menu_content.antidote = new Button(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 + 55, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_antidote"), Content.Load<Texture2D>(@"SpellButtons\sp_antidote"), Content.Load<Texture2D>(@"SpellButtons\sp_antidote_p"), new Event(Events.AddSpell_Antidote), "", "Противоядие", "Переводит какого-либо персонажа из состояния \"отравлен\" в состояние \"здоров или ослаблен\". Cost: 30MP");
            sl_menu_content.move = new Button(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 + 120, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_unfreeze"), Content.Load<Texture2D>(@"SpellButtons\sp_unfreeze"), Content.Load<Texture2D>(@"SpellButtons\sp_unfreeze_p"), new Event(Events.AddSpell_Move), "", "Отомри!", "Переводит какого-либо персонажа из состояния \"парализован\" в состояние \"здоров или ослаблен\". Текущая величина здоровья становится равной 1HP. Cost: 85MP");
            sl_menu_content.revive = new Button(new Rectangle(viewp.Width / 2 - 57, viewp.Height / 2 - 75, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_resurrection"), Content.Load<Texture2D>(@"SpellButtons\sp_resurrection"), Content.Load<Texture2D>(@"SpellButtons\sp_resurrection_p"), new Event(Events.AddSpell_Revive), "", "Оживить", "Переводит какого-либо как же я заебался это писать персонажа из состояния \"мертв\" в состояние \"здоров или ослаблен\". Cost: 150MP");
            sl_menu_content.shield = new Button(new Rectangle(viewp.Width / 2 - 57, viewp.Height / 2 - 10, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_armor"), Content.Load<Texture2D>(@"SpellButtons\sp_armor"), Content.Load<Texture2D>(@"SpellButtons\sp_armor_p"), new Event(Events.AddSpell_Shield), "", "Броня", "Персонаж, на которого обращено заклинание, становится неуязвимым в течение некоторого промежутка времени, определяемого силой заклинания. Cost: 1s - 50MP");
            sl_menu_content.add_hp_l = new Content(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 - 75, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_heal_l"));
            sl_menu_content.heal_l = new Content(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 - 10, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_cure_l"));
            sl_menu_content.antidote_l = new Content(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 + 55, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_antidote_l"));
            sl_menu_content.move_l = new Content(new Rectangle(viewp.Width / 2 - 126, viewp.Height / 2 + 120, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_unfreeze_l"));
            sl_menu_content.revive_l = new Content(new Rectangle(viewp.Width / 2 - 57, viewp.Height / 2 - 75, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_resurrection_l"));
            sl_menu_content.shield_l = new Content(new Rectangle(viewp.Width / 2 - 57, viewp.Height / 2 - 10, 40, 40), Content.Load<Texture2D>(@"SpellButtons\sp_armor_l"));
            button_content.infoBox = Content.Load<Texture2D>(@"GUI\infoBox");
            game_content.enemy = new Enemy(new Vector2(800, 800), 1, new Animation(100, Content.Load<Texture2D>("enemy"), 30, 32, 3), 1, 2, 20, 200, Content.Load<Texture2D>("Dead_Enemy_Txtr"));
            game_content.baryga = new Enemy(new Vector2(1300, 800), 1, new Animation(100, Content.Load<Texture2D>("baryga_txtr"), 30, 32, 3), 1, 2, 24, 400, Content.Load<Texture2D>("dead_baryga"));
            game_content.kolshik = new Enemy(new Vector2(500, 900), 1, new Animation(100, Content.Load<Texture2D>("kolshik_txtr"), 30, 32, 3), 1, 2, 15, 200, Content.Load<Texture2D>("dead_kolshik"));
            game_content.petuh = new Enemy(new Vector2(1100, 1400), 1, new Animation(100, Content.Load<Texture2D>("petuh_txtr"), 30, 32, 3), 1, 2, 10, 150, Content.Load<Texture2D>("dead_petuh"));
            game_content.character = new Mage_Character("pesos", Races.Baryga, "Male", new Animation(100, Content.Load<Texture2D>("gopnik_texture"), 30, 32, 3), new Vector2(300, 300), 1, 1);
            game_content.fight_txtr = Content.Load<Texture2D>("fighttxtr");
            game_content.dead_enemy_txtr = Content.Load<Texture2D>("Dead_Enemy_Txtr");
            game_content.beer = Content.Load<Texture2D>("beer");
            game_content.boyarishnik = Content.Load<Texture2D>("boyarishnik");
            game_content.rose = Content.Load<Texture2D>("rose");
            game_content.balanda = Content.Load<Texture2D>("balanda");
            game_content.colesa = Content.Load<Texture2D>("colesa");
            game_content.playBoy = Content.Load<Texture2D>("playboy");
            cursor_states.Normal = Content.Load<Texture2D>("cursor");
            cursor_states.Choosing = Content.Load<Texture2D>("choose_cursor");
            game_gui_content.BigHP = new Button(new Rectangle(-50, -50, 50, 50), game_content.beer, game_content.beer, game_content.beer, Events.Use_BigHP, "", "Пивас", "Живительный хмельной напиток. Добавляет 50 HP персонажу.", new Vector2(170,180));
            game_gui_content.MedHP = new Button(new Rectangle(-50, -50, 50, 50), game_content.beer, game_content.beer, game_content.beer, Events.Use_MedHP, "", "Пивас", "Живительный хмельной напиток. Добавляет 25 HP персонажу.", new Vector2(170, 180));
            game_gui_content.SmaHP = new Button(new Rectangle(-50, -50, 50, 50), game_content.beer, game_content.beer, game_content.beer, Events.Use_SmaHP, "", "Пивас", "Живительный хмельной напиток. Добавляет 10 HP персонажу.", new Vector2(170, 180));
            game_gui_content.BigMP = new Button(new Rectangle(-50, -50, 50, 50), game_content.boyarishnik, game_content.boyarishnik, game_content.boyarishnik, Events.Use_BigMP, "", "Боярышник", "Напиток прозрения, дарующий философские видения каждому, кто хоть немного опробует его. Добавляет 50 MP персонажу.", new Vector2(170, 180));
            game_gui_content.MedMP = new Button(new Rectangle(-50, -50, 50, 50), game_content.boyarishnik, game_content.boyarishnik, game_content.boyarishnik, Events.Use_MedMP, "", "Боярышник", "Напиток прозрения, дарующий философские видения каждому, кто хоть немного опробует его. Добавляет 25 MP персонажу.", new Vector2(170, 180));
            game_gui_content.SmaMP = new Button(new Rectangle(-50, -50, 50, 50), game_content.boyarishnik, game_content.boyarishnik, game_content.boyarishnik, Events.Use_SmaMP, "", "Боярышник", "Напиток прозрения, дарующий философские видения каждому, кто хоть немного опробует его. Добавляет 10 MP персонажу.", new Vector2(170, 180));
            game_gui_content.Rose = new Button(new Rectangle(-50, -50, 50, 50), game_content.rose, game_content.rose, game_content.rose, Events.Use_Rose, "", "Розочка", "Оружие настоящих дворовых джентельменов. Издавна используется в целях нападения и самообороны. Урон атакуемому персонажу, в зависимости от силы удара", new Vector2(170, 180));
            game_gui_content.Balanda = new Button(new Rectangle(-50, -50, 50, 50), game_content.balanda, game_content.balanda, game_content.balanda, Events.Use_Balanda, "", "Тухлая баланда", "Некогда - блюдо эстетов. Нынче - неприятная консистенция. Можно вылить на врага. Отравляет врага и наносит урон в зависимости от мощности.", new Vector2(170, 180));
            game_gui_content.Colesa = new Button(new Rectangle(-50, -50, 50, 50), game_content.colesa, game_content.colesa, game_content.colesa, Events.Use_Colesa, "", "Колеса", "Фармацевтический препарат, неизвестного назначения. Принимать не желательно, если только вы не больны. Излечивает персонажа, на которого они были \nиспользованы.", new Vector2(170, 180));
            game_gui_content.PlayBoy = new Button(new Rectangle(-50, -50, 50, 50), game_content.playBoy, game_content.playBoy, game_content.playBoy, Events.Use_PlayBoy, "", "PlayBoy", "Прекрасный выпуск хорошего журнала. Осторожно, элементы содержания могут нести оцепеняющий характер. Заставляет застыть персонажа, на которого использован.", new Vector2(170, 180));
        }
    }
}