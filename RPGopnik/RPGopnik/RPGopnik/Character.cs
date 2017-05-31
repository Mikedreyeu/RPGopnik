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
    enum Conditions { Normal, Weak, Sick, Poisoned, Paralyzed, Dead };
    enum Races { Gopnik, Petuh, Kolshik, Baryga };
    enum Direction { Down, Left, Right, Up };

    class Character : Person_Abstract 
    {
        public Inventory inventory;
        Animation animation;
        Direction direction;
        private readonly uint id;
        private static uint next_id = 1;
        private readonly string name;
        private readonly Races race;
        private readonly string gender;
        private int age;    
        private bool can_speak;
        private bool can_move;
        private int max_velocity;
        private Vector2 velocity_now = new Vector2(0, 0);
        public uint AttackDamage;
        bool attacking = false;

        public Rectangle Rect
        {
            get { return new Rectangle((int)pos.X, (int)pos.Y, 30, 32); }
        }
        public uint ID
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
        }
        public Races Race
        {
            get { return race; }
        }
        public string Gender
        {
            get { return gender; }
        }
        public int Age
        {
            get { return age; }
            private set { age = value; }
        }
        public bool Can_Speak
        {
            get { return can_speak; }
            private set { can_speak = value; }
        }
        public bool Can_Move
        {
            get { return can_move; }
            private set { can_move = value; }
        }
        public void Condition_Check(Character ch)
        {
            double percentage = ch.Curr_HP / ch.Max_HP;
            if (percentage > 0 && percentage < 0.1)
                ch.Condition = (byte)Conditions.Weak;
            else if (percentage >= 0.1)
                ch.Condition = (byte)Conditions.Normal;
            else if (percentage == 0)
                ch.Condition = (byte)Conditions.Dead;
        }
        public Character(string name_, Races race_, string gender_, Animation animation, Vector2 pos, int max_velocity, uint attackDamage)
        {
            inventory = new Inventory(this);
            direction = Direction.Down;
            this.max_hp = 100;
            curr_hp = 50;
            this.pos = pos;
            this.animation = animation;
            this.id = next_id++;
            this.name = name_;
            this.race = race_;
            this.gender = gender_;
            this.max_velocity = max_velocity;
            this.AttackDamage = attackDamage;
        }
        public string Character_Info(Character ch)
        {
            return "Имя: " + ch.Name + "\nРаса: " + ch.Race + "\nПол: " + ch.Gender +
                "\nВозраст: " + ch.Age + "\nИдентификатор: " + ch.ID + "\nТекущие очки здоровья: " +
                ch.Curr_HP + "\nТекущие очки опыта: " + ch.XP;
        }

        private bool is_wasd(Keys key)
        {
            if (key == Keys.W || key == Keys.A || key == Keys.S || key == Keys.D)
                return true;
            return false;
        }

        public void Update(Camera camera, List<Rectangle> collisionObjects, Enemy enemy)
        {
            inventory.Update(camera, this);
            if (condition != (byte)Conditions.Dead && condition != (byte)Conditions.Paralyzed)
            {
                velocity_now = Vector2.Zero;
                if (Keyboard.GetState().GetPressedKeys().Count(is_wasd) > 0)
                    animation.Update();
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    direction = Direction.Up;
                    velocity_now.Y = -max_velocity;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    direction = Direction.Down;
                    velocity_now.Y = max_velocity;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    direction = Direction.Left;
                    velocity_now.X = -max_velocity;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    direction = Direction.Right;
                    velocity_now.X = max_velocity;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Q) && Enemy.distanceToTheCharacter <= 42)
                {
                    attacking = true;
                    if (enemy.Curr_HP > 0)
                        enemy.Curr_HP -= ContentLoader.game_content.character.AttackDamage;
                    else
                    {
                        enemy.Condition = (byte)Conditions.Dead;
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
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (!attacking)
            {
                animation.Draw(spritebatch, pos, direction);
            }
            else
            {
                spritebatch.Draw(ContentLoader.game_content.fight_txtr, Rect, Color.White);
                attacking = false;
            }
            inventory.Draw(spritebatch, this);
        }
    }

    class Mage_Character : Character
    {
        private uint curr_mana;
        private uint max_mana;

        public uint Curr_Mana
        {
            get { return curr_mana; }
            set { curr_mana = value; }
        }
        public uint Max_Mana
        {
            get { return max_mana; }
            set { max_mana = value; }
        }
        public Mage_Character(string name, Races race, string gender, Animation animation, Vector2 pos, int max_velocity, uint AttackDamage) : base(name, race, gender, animation, pos, max_velocity, AttackDamage)
        {
            max_mana = 100;
            curr_mana = 100;
        }
        public void Add_HP_Spell(Character ch)
        {
            if (this.Curr_Mana >= 2 * (ch.Max_HP - ch.Curr_HP))
            {
                ch.Curr_HP = ch.Max_HP;
                this.Curr_Mana -= 2 * (ch.Max_HP - ch.Curr_HP);
            }
            else
            {
                uint change = this.Curr_Mana / 2;
                ch.Curr_HP += change;
                this.Curr_Mana -= change * 2;
            }
        }
    }
}
