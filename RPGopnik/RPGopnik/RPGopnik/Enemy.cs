using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Enemy
    {
        public Vector2 pos;
        Animation animation;
        Random rand;
        private int max_velocity;
        private Vector2 velocity_now = new Vector2(0,0);
        public Direction Enemy_Direction = Direction.Down;
        public Rectangle Rect
        {
            get { return new Rectangle((int)pos.X, (int)pos.Y, 30, 32); }
        }

        public Enemy(Vector2 pos, int max_velocity, Animation animation)
        {
            rand = new Random();
            this.animation = animation;
            this.pos = pos;
            this.max_velocity = max_velocity;
        }

        public void Update(Character character, List<Rectangle> collisionObjects)
        {
            velocity_now = Vector2.Zero;
            double range = Math.Sqrt(Math.Pow((this.pos.X - character.pos.X), 2) + Math.Pow((this.pos.Y - character.pos.Y), 2));
            if (range <= 300 && range >= 40)
            {
                animation.Update();
                if (this.pos.X > character.pos.X && this.pos.Y > character.pos.Y)
                {
                    velocity_now.X = -max_velocity;
                    velocity_now.Y = -max_velocity;
                    Enemy_Direction = Direction.Left;
                }
                else if (this.pos.X > character.pos.X && this.pos.Y < character.pos.Y)
                {
                    velocity_now.X = -max_velocity;
                    velocity_now.Y = max_velocity;
                    Enemy_Direction = Direction.Left;
                }
                else if (this.pos.X < character.pos.X && this.pos.Y > character.pos.Y)
                {
                    velocity_now.X = max_velocity;
                    velocity_now.Y = -max_velocity;
                    Enemy_Direction = Direction.Right;
                }
                else if (this.pos.X < character.pos.X && this.pos.Y < character.pos.Y)
                {
                    velocity_now.X = max_velocity;
                    velocity_now.Y = max_velocity;
                    Enemy_Direction = Direction.Right;
                }
                else if (this.pos.X == character.pos.X && this.pos.Y > character.pos.Y)
                {
                    velocity_now.Y = -max_velocity;
                    velocity_now.X = 0;
                    Enemy_Direction = Direction.Up;
                }
                else if (this.pos.X == character.pos.X && this.pos.Y < character.pos.Y)
                {
                    velocity_now.Y = max_velocity;
                    velocity_now.X = 0;
                    Enemy_Direction = Direction.Down;
                }
                else if (this.pos.Y == character.pos.Y && this.pos.X < character.pos.X)
                {
                    velocity_now.X = max_velocity;
                    velocity_now.Y = 0;
                    Enemy_Direction = Direction.Right;
                }
                else if (this.pos.Y == character.pos.Y && this.pos.X > character.pos.X)
                {
                    velocity_now.X = -max_velocity;
                    velocity_now.Y = 0;
                    Enemy_Direction = Direction.Left;
                }
            }

            #region Collision Handling Region
            foreach (Rectangle co in collisionObjects)
            {

                if (velocity_now.X > 0 && Rect.Right + velocity_now.X > co.Left && Rect.Left < co.Left
                    && Rect.Bottom > co.Top && Rect.Top < co.Bottom)
                {
                    velocity_now.X = co.Left - Rect.Right;
                }
                if (velocity_now.X < 0 && Rect.Left + velocity_now.X < co.Right && Rect.Right > co.Right
                    && Rect.Bottom > co.Top && Rect.Top < co.Bottom)
                {
                    velocity_now.X = co.Right - Rect.Left;
                }
                if (velocity_now.Y > 0 && Rect.Bottom + velocity_now.Y > co.Top && Rect.Top < co.Top
                    && Rect.Right > co.Left && Rect.Left < co.Right)
                {
                    velocity_now.Y = co.Top - Rect.Bottom;
                }
                if (velocity_now.Y < 0 && Rect.Top + velocity_now.Y < co.Bottom && Rect.Bottom > co.Bottom
                    && Rect.Right > co.Left && Rect.Left < co.Right)
                {
                    velocity_now.Y = co.Bottom - Rect.Top;
                }

                if (velocity_now.X < 0 && velocity_now.Y < 0
                    && Rect.Top + velocity_now.Y < co.Bottom && Rect.Left + velocity_now.X < co.Right
                    && Rect.Bottom > co.Top && Rect.Right > co.Left)
                {
                    velocity_now.X = co.Right - Rect.Left;
                    velocity_now.Y = co.Bottom - Rect.Top;
                }
                if (velocity_now.X > 0 && velocity_now.Y > 0
                    && Rect.Bottom + velocity_now.Y > co.Top && Rect.Right + velocity_now.X > co.Left
                    && Rect.Top < co.Bottom && Rect.Left < co.Right)
                {
                    velocity_now.X = co.Left - Rect.Right;
                    velocity_now.Y = co.Top - Rect.Bottom;
                }
                if (velocity_now.X > 0 && velocity_now.Y < 0
                    && Rect.Top + velocity_now.Y < co.Bottom && Rect.Right + velocity_now.X > co.Left
                    && Rect.Bottom > co.Top && Rect.Left < co.Right)
                {
                    velocity_now.X = co.Left - Rect.Right;
                    velocity_now.Y = co.Bottom - Rect.Top;
                }
                if (velocity_now.X < 0 && velocity_now.Y > 0
                    && Rect.Left + velocity_now.X < co.Right && Rect.Bottom + velocity_now.Y > co.Top
                    && Rect.Top < co.Bottom && Rect.Right > co.Left)
                {
                    velocity_now.X = co.Right - Rect.Left;
                    velocity_now.Y = co.Top - Rect.Bottom;
                }
            }
            #endregion

            pos += velocity_now;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            animation.Draw(spritebatch, pos, Enemy_Direction);
        }
    }
}
