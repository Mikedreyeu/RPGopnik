﻿using System;
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

    class Character : IComparable 
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
        private int xp;
        private uint curr_hp;
        private uint max_hp;
        private bool can_speak;
        private bool can_move;
        private byte condition;
        public Vector2 pos;
        private int max_velocity;
        private Vector2 velocity_now = new Vector2(0, 0);

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
            set { age = value; }
        }
        public uint Curr_HP
        {
            get { return curr_hp; }
            set { curr_hp = value; }
        }
        public uint Max_HP
        {
            get { return max_hp; }
            set { max_hp = value; }
        }
        public int XP
        {
            get { return xp; }
            set { xp = value; }
        }
        public byte Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public bool Can_Speak
        {
            get { return can_speak; }
            set { can_speak = value; }
        }
        public bool Can_Move
        {
            get { return can_move; }
            set { can_move = value; }
        }
        public int CompareTo(object ch2)
        {
            return this.XP.CompareTo((ch2 as Character).XP);
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
        public Character(string name_, Races race_, string gender_, Animation animation, Vector2 pos, int max_velocity)
        {
            inventory = new Inventory();
            direction = Direction.Down;
            this.max_hp = 100;
            this.pos = pos;
            this.animation = animation;
            this.id = next_id++;
            this.name = name_;
            this.race = race_;
            this.gender = gender_;
            this.max_velocity = max_velocity;

        }
        public string Character_Info(Character ch)
        {
            return "Имя: " + ch.Name + "\nРаса: " + ch.Race + "\nПол: " + ch.Gender +
                "\nВозраст: " + ch.Age + "\nИдентификатор: " + ch.ID + "\nТекущие очки здоровья: " +
                ch.Curr_HP + "\nТекущие очки опыта: " + ch.XP;
        }
        public void Update(List<Rectangle> collisionObjects)
        {
            if (condition != (byte)Conditions.Dead)
            {
                velocity_now = Vector2.Zero;
                if (Keyboard.GetState().GetPressedKeys().Length != 0)
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
                        velocity_now.Y = co.Left - Rect.Top;
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
            animation.Draw(spritebatch, pos, direction);
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
        public Mage_Character(string name, Races race, string gender, Animation animation, Vector2 pos, int max_velocity) : base(name, race, gender, animation, pos, max_velocity)
        {
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
