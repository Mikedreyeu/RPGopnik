using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    enum Direction { Down, Left, Right, Up };
    class Enemy
    {
        int x, y;
        Animation animation;
        private int max_velocity;
        private Point velocity_now = new Point(0,0);
        public Direction Enemy_Direction = Direction.Down;
        public Enemy(int x, int y, int max_velocity, Animation animation)
        {
            this.animation = animation;
            this.x = x;
            this.y = y;
            this.max_velocity = max_velocity;
        }

        public void Update(MouseState mouse)
        {
            velocity_now.Y = 0;
            velocity_now.X = 0;
            if (Keyboard.GetState().GetPressedKeys().Length != 0)
            {
                animation.Update();
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    Enemy_Direction = Direction.Up;
                    velocity_now.Y = -max_velocity;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    Enemy_Direction = Direction.Down;
                    velocity_now.Y = max_velocity;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    Enemy_Direction = Direction.Left;
                    velocity_now.X = -max_velocity;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    Enemy_Direction = Direction.Right;
                    velocity_now.X = max_velocity;
                }
                x += velocity_now.X;
                y += velocity_now.Y;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            animation.Draw(spritebatch, x, y, Enemy_Direction);
        }
    }
}
