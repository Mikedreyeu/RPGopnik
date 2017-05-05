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
            Menu.background = new Background(Content.Load<Texture2D>("background"));
            Button.font = Content.Load<SpriteFont>("bt_font");
            main = new Menu(new List<RPGopnik.Content> { new Content(new Rectangle(50, 0, 700, 200), Content.Load<Texture2D>(@"GUI\logo")),
                                                         new Button(new Rectangle(200, 230, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.game), "Game"),
                                                         new Button(new Rectangle(200, 300, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.help), "Help"),
                                                         new Button(new Rectangle(200, 370, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.exit), "Exit")});
            help = new Menu(new List<RPGopnik.Content> { new Content(new Rectangle(50, 200, 700, 300), Content.Load<Texture2D>("help")),
                                                         new Button(new Rectangle(200, 500, 400, 70), Content.Load<Texture2D>(@"GUI\main"), Content.Load<Texture2D>(@"GUI\hover"), Content.Load<Texture2D>(@"GUI\pressed"), new Event(Events.main), "Main menu")});
            spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new Map(@"Content\mainArea.tmx");
            map.LoadContent(Content);
            game = new Game(map, new Enemy(new Rectangle(100, 100, 30, 32), Content.Load<Texture2D>("enemy")));
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
