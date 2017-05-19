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
        }

        public struct Menu_Content
        {
            public Background background;
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

        public Menu_Content menu_content;
        public Help_Menu_Content help_menu_content;
        public Main_Menu_Content main_menu_content;
        public Game_Content game_content;

        public ContentLoader(ContentManager Content, Viewport viewp)
        {
            help_menu_content.info = new Content(new Rectangle(viewp.Width / 2 - 350, viewp.Height - 400, 700, 250), Content.Load<Texture2D>("help"));
            help_menu_content.main_menu = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height - 100, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.main), "Main menu");
            menu_content.logo = new Content(new Rectangle(viewp.Width / 2 - 437, viewp.Height / 15, 874, 250), Content.Load<Texture2D>(@"GUI\logo"));
            menu_content.background = new Background(Content.Load<Texture2D>("title_screen_background"));
            main_menu_content.game_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height / 2, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.game), "Game");
            main_menu_content.help_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height / 2 + 70, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.help), "Help");
            main_menu_content.exit_button = new Button(new Rectangle(viewp.Width / 2 - 200, viewp.Height / 2 + 140, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.exit), "Exit");
            game_content.enemy = new Enemy(new Vector2(100, 100), 3, new Animation(100, Content.Load<Texture2D>("enemy"), 30, 32, 3));
            game_content.character = new Character("pesos", Races.Baryga, "Male", new Animation(100, Content.Load<Texture2D>("gopnik_texture"), 30, 32, 3), new Vector2(300, 300), 2);
        }
    }
}
