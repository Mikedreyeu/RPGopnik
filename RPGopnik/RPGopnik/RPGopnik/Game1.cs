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
            Button.font = Content.Load<SpriteFont>("bt_font");
            main = new MainMenu();
            help = new HelpMenu();
            pause = new Pause();
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
                    game.Draw(spriteBatch);
                    break;
                case gs.PAUSE:
                    pause.Update();
                    game.Draw(spriteBatch);
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
            game = new Game(map, GraphicsDevice.Viewport, ContentLoader.game_content.character, new List<Artefact> { new Pivas(Artefact.Size.Big, new Vector2(400, 300)) });
            game.LoadContent(Content); // просто впихнул
        }
    }
}
