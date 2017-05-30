using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Enemy : Person_Abstract
    {
        public Animation animation;
        Random rand;
        uint attackDamage;
        bool drawAttack = true;
        double attackTimer, attackInterval;
        public static double distanceToTheCharacter;
        public int max_velocity;
        private Vector2 velocity_now = new Vector2(0, 0);
        public Direction Enemy_Direction = Direction.Down;
        Texture2D dead_txtr;
        public int XP_For_Killing;
        bool xp_Isnt_Added = true;

        public Rectangle Rect
        {
            get { return new Rectangle((int)pos.X, (int)pos.Y, 30, 32); }
        }

        public Enemy(Vector2 pos, int max_velocity, Animation animation, uint attackDamage, double attackInterval, uint start_hp, int xp_for_killing, Texture2D dead_txtr)
        {
            rand = new Random();
            this.animation = animation;
            this.pos = pos;
            this.max_velocity = max_velocity;
            this.attackDamage = attackDamage;
            this.attackInterval = attackInterval;
            this.curr_hp = start_hp;
            this.XP_For_Killing = xp_for_killing;
            this.dead_txtr = dead_txtr;
        }

        public void Update(GameTime gameTime, Character character, List<Rectangle> collisionObjects)
        {
            velocity_now = Vector2.Zero;
            distanceToTheCharacter = Math.Sqrt(Math.Pow((this.pos.X - character.pos.X), 2) + Math.Pow((this.pos.Y - character.pos.Y), 2));
            if (distanceToTheCharacter <= 300 && distanceToTheCharacter >= 40)
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

            attackTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (attackTimer < 0)
            {
                attackTimer = attackInterval;
                if (distanceToTheCharacter <= 100)
                {
                    character.Curr_HP -= attackDamage;
                    drawAttack = true;
                }
                else
                {
                    drawAttack = false;
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

        public void Draw(SpriteBatch spritebatch, Enemy enemy)
        {
            if (this.Condition != (byte)Conditions.Dead)
            {
                animation.Draw(spritebatch, pos, Enemy_Direction);
                if (attackTimer > attackInterval / 2 && drawAttack == true)
                {
                    spritebatch.DrawString(ContentLoader.game_gui_content.hp_mana_font, "molodoy chelovek proydomte", new Vector2(pos.X - 110, pos.Y - 25), Color.White, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);
                }
            }
            else if (xp_Isnt_Added)
            {
                xp_Isnt_Added = false;
                this.Condition = (byte)Conditions.Dead;
                this.max_velocity = 0;
                this.attackDamage = 0;
                ContentLoader.game_content.character.XP += this.XP_For_Killing;
            }
            else if (!xp_Isnt_Added && this.Condition == (byte)Conditions.Dead)
            {
                spritebatch.Draw(enemy.dead_txtr, enemy.pos, Color.White);
            }
        }
    }
}
