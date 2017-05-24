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
        ContentLoader content_loader;
        Menu main, help, pause;
        Map map;
        Game game;

        public Game1()
        { 
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Events.g = this;
            LoadNewGame();
            Pivas.texture = content_loader.game_content.beer;
            Button.font = Content.Load<SpriteFont>("bt_font");
            main = new Menu(new List<Content> { content_loader.menu_content.background,
                                                         content_loader.menu_content.logo,
                                                         content_loader.main_menu_content.game_button,
                                                         content_loader.main_menu_content.help_button,
                                                         content_loader.main_menu_content.exit_button});
            help = new Menu(new List<Content> { content_loader.menu_content.background,
                                                         content_loader.menu_content.logo,
                                                         content_loader.help_menu_content.info,
                                                         content_loader.help_menu_content.main_menu});
            pause = new Menu(new List<Content> { content_loader.pause_content.background,
                                                         content_loader.pause_content.pause_header,
                                                         content_loader.pause_menu_content.resume_button,
                                                         content_loader.pause_menu_content.exit_button});
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (game_state == gs.GAME && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                game_state = gs.PAUSE;
            }
            switch (game_state)
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
                    game.Draw(spriteBatch, content_loader.game_gui_content.foundation);
                    break;
                case gs.PAUSE:
                    pause.Update();
                    game.Draw(spriteBatch, content_loader.game_gui_content.foundation);
                    pause.Draw(spriteBatch);
                    if (game_state == gs.MAIN)
                    {
                        LoadNewGame();
                    }
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private void LoadNewGame()
        {
            content_loader = new ContentLoader(Content, GraphicsDevice.Viewport);
            map = new Map(@"Content\StartingArea.tmx");
            map.LoadContent(Content);
            game = new Game(map, GraphicsDevice.Viewport, content_loader.game_content.character, new List<Artefact> { new Pivas(Artefact.Size.Big, new Vector2(400, 300)) });
            game.LoadContent(Content); // просто впихнул
        }
    }
}
