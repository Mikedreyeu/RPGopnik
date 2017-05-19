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
        public Enemy(Vector2 pos, int max_velocity, Animation animation)
        {
            rand = new Random();
            this.animation = animation;
            this.pos = pos;
            this.max_velocity = max_velocity;
        }

        public void Update(MouseState mouse)
        {
            velocity_now = Vector2.Zero;
            Enemy_Direction = (Direction)rand.Next(4);
            switch(Enemy_Direction)
            {
                case Direction.Down:
                    velocity_now.Y = max_velocity;
                    break;
                case Direction.Up:
                    velocity_now.Y = -max_velocity;
                    break;
                case Direction.Right:
                    velocity_now.X = max_velocity;
                    break;
                case Direction.Left:
                    velocity_now.X = -max_velocity;
                    break;
            }
            pos += velocity_now;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            animation.Draw(spritebatch, pos, Enemy_Direction);
        }
    }
}
