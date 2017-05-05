using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Enemy : Content
    {
        private Point velocity = new Point(0,0);
        public enum Direction {Up, Down, Left, Right};
        public Direction Enemy_Direction = Direction.Down;
        public Enemy(Rectangle rectangle, Texture2D texture) : base(rectangle, texture) { }

        public override void Update(MouseState mouse)
        {
            velocity.Y = 0;
            velocity.X = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Enemy_Direction = Direction.Up;
                velocity.Y = -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Enemy_Direction = Direction.Down;
                velocity.Y = 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Enemy_Direction = Direction.Left;
                velocity.X = -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Enemy_Direction = Direction.Right;
                velocity.X = 1;
            }
            rect.Location = new Point(rect.Location.X + velocity.X, rect.Location.Y + velocity.Y);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            switch(Enemy_Direction)
            {
                case Direction.Down:
                    spritebatch.Draw(main_texture, rect, new Rectangle(30, 0, 30, 32), Color.White);
                    break;
                case Direction.Left:
                    spritebatch.Draw(main_texture, rect, new Rectangle(30, 32, 30, 32), Color.White);
                    break;
                case Direction.Right:
                    spritebatch.Draw(main_texture, rect, new Rectangle(30, 64, 30, 32), Color.White);
                    break;
                case Direction.Up:
                    spritebatch.Draw(main_texture, rect, new Rectangle(30, 96, 30, 32), Color.White);
                    break;
            }
        }
    }
}
