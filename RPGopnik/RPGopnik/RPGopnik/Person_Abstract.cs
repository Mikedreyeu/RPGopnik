using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RPGopnik
{
    abstract class Person_Abstract : IComparable
    {
        protected int xp;
        protected byte condition;
        protected uint curr_hp;
        protected uint max_hp;
        public Vector2 pos;

        public byte Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public int XP
        {
            get { return xp; }
            set { xp = value; }
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

        public int CompareTo(object ch2)
        {
            return this.XP.CompareTo((ch2 as Person_Abstract).XP);
        }
    }
}
