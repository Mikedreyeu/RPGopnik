﻿using System;
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
        public int x, y;
        Animation animation;
        Random rand;
        private int max_velocity;
        private Point velocity_now = new Point(0,0);
        public Direction Enemy_Direction = Direction.Down;
        public Enemy(int x, int y, int max_velocity, Animation animation)
        {
            rand = new Random();
            this.animation = animation;
            this.x = x;
            this.y = y;
            this.max_velocity = max_velocity;
        }

        public void Update(MouseState mouse)
        {
            velocity_now.Y = 0;
            velocity_now.X = 0;
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
            x += velocity_now.X;
            y += velocity_now.Y;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            animation.Draw(spritebatch, x, y, Enemy_Direction);
        }
    }
}
