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
    public enum gs { MAIN, HELP, PAUSE, GAME, SLSCREEN, STORY1, STORY2, END };
    delegate void Event();

    class Game1 : Microsoft.Xna.Framework.Game
    {
        public static gs game_state = gs.MAIN; 
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Menu main, help, pause, slScreen, story1, story2, end;
        Map map;
        public Game game;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
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
            story1 = new Story1();
            story2 = new Story2();
            end = new EndMenu();
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
                    game.Update(gameTime);
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
                case gs.SLSCREEN:
                    slScreen.Update();
                    game.Draw(spriteBatch);
                    slScreen.Draw(spriteBatch);
                    break;
                case gs.STORY1:
                    story1.Update();
                    story1.Draw(spriteBatch);
                    break;
                case gs.STORY2:
                    story2.Update();
                    story2.Draw(spriteBatch);
                    break;
                case gs.END:
                    end.Update();
                    end.Draw(spriteBatch);
                    break;
            }
            Cursor.Draw(spriteBatch);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void LoadNewGame()
        {
            ContentLoader.Load(Content, GraphicsDevice.Viewport);
            map = new Map(@"Content\StartingArea.tmx");
            map.LoadContent(Content);
            List<Enemy> list = new List<Enemy> { ContentLoader.game_content.enemy, ContentLoader.game_content.baryga, ContentLoader.game_content.kolshik, ContentLoader.game_content.petuh };
            game = new Game(map, list, GraphicsDevice.Viewport, ContentLoader.game_content.character, new List<Artefact> { new Pivas(Artefact.Size.Big, new Vector2(1200, 300)), new Boyarishnik(Artefact.Size.Little, new Vector2(300, 900)), new Rose(10, new Vector2(600, 600)), new Colesa(new Vector2(350, 350)), new Balanda(10, new Vector2(1000, 1000)), new PlayBoy(new Vector2(800, 450)) });
            slScreen = new SLScreen(game);
        }
    }
}
