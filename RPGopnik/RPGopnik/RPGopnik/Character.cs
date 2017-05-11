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

    class Character : IComparable 
    {
        private Rectangle rect;
        private Texture2D main_texture;
        private readonly uint id;
        private static uint next_id = 1;
        private readonly string name;
        private readonly string race;
        private readonly string gender;
        private int age;
        private int xp;
        private uint curr_hp;
        private uint max_hp;
        private bool can_speak;
        private bool can_move;
        private byte condition;
        public uint ID
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
        }
        public string Race
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
        public Character(string name_, string race_, string gender_)
        {
            this.id = next_id++;
            this.name = name_;
            this.race = race_;
            this.gender = gender_;

        }
        public string Character_Info(Character ch)
        {
            return "Имя: " + ch.Name + "\nРаса: " + ch.Race + "\nПол: " + ch.Gender +
                "\nВозраст: " + ch.Age + "\nИдентификатор: " + ch.ID + "\nТекущие очки здоровья: " +
                ch.Curr_HP + "\nТекущие очки опыта: " + ch.XP;
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
        public Mage_Character(string name, string race, string gender) : base(name, race, gender)
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
