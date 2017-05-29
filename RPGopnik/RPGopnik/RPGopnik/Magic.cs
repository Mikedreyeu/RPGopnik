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
    interface IMagic
    {
        void Use(Character user);
        void Use(Character user, Character character);
        void Use(Character user, uint power);
        void Use(Character user, Character character, uint power);
    }

    abstract class Artefact : IMagic
    {
        public enum Type { BigHP, MedHP, SmaHP, BigMP, MedMP, SmaMP, Rose, Colesa, Balanda, PlayBoy }
        public Vector2 pos;
        public enum Size { Little = 10, Middle = 25, Big = 50 }
        public uint power;
        public uint max_power;
        public bool renewable;
        public void Use(Character user)
        {
            Use(user, user, 1);
        }
        public void Use(Character user, Character character)
        {
            Use(user, character, 1);
        }
        public void Use(Character user, uint power)
        {
            Use(user, user, power);
        }
        public virtual void Use(Character user, Character character, uint power) { }

        public virtual void Draw(SpriteBatch spritebatch) { }
    }

    abstract class Spell : IMagic
    {
        public enum Type { AddHp, Heal, Move, Antidote, Shield, Revieve };
        public Type type;
        public uint MinMana { get { return minMana; } }
        protected uint minMana;
        protected bool canSpeakRequired;
        protected bool canMoveRequired;
        protected Spell(uint minMana, bool canSpeakRequired, bool canMoveRequired)
        {
            this.minMana = minMana;
            this.canSpeakRequired = canSpeakRequired;
            this.canMoveRequired = canMoveRequired;
        }
        public void Use(Character user)
        {
            Use(user, user, 1);
        }
        public void Use(Character user, Character character)
        {
            Use(user, character, 1);
        }
        public void Use(Character user, uint power)
        {
            Use(user, user, power);
        }
        public virtual void Use(Character user, Character character, uint power)
        { }
    }


}
