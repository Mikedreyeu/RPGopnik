using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RPGopnik
{
    public enum gs { MAIN, HELP, PAUSE, GAME };
    delegate void Event();

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static gs game_state = gs.MAIN; 
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Menu main, help;
        Map map;
        Game game;

        public Game1()
        { 
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Events.g = this;
            ContentLoader content_loader = new ContentLoader(Content);
            Menu.background = content_loader.menu_content.background;
            Button.font = Content.Load<SpriteFont>("bt_font");
            main = new Menu(new List<RPGopnik.Content> { content_loader.menu_content.logo,
                                                         content_loader.main_menu_content.game_button,
                                                         content_loader.main_menu_content.help_button,
                                                         content_loader.main_menu_content.exit_button});
            help = new Menu(new List<RPGopnik.Content> { content_loader.menu_content.logo,
                                                         content_loader.help_menu_content.info,
                                                         content_loader.help_menu_content.main_menu});
            spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new Map(@"Content\StartArea.tmx");
            map.LoadContent(Content);
            game = new Game(map, content_loader.game_content.character);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        { 
            switch(game_state)
            {
                case gs.MAIN:
                    main.Update();
                    main.Draw(spriteBatch);
                    break;
                case gs.HELP:
                    help.Update();
                    help.Draw(spriteBatch);
                    break;
                case gs.GAME:
                    game.Update();
                    game.Draw(spriteBatch);
                    break;
                case gs.PAUSE:
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
