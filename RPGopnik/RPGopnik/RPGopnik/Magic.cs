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
        void Use(Character user, Person_Abstract character);
        void Use(Character user, uint power);
        void Use(Character user, Person_Abstract character, uint power);
        bool powerable { get; set; }
        bool choosable { get; set; }
    }

    abstract class Artefact : IMagic
    {
        public bool powerable { get; set; }
        public bool choosable { get; set; }
        public enum Type { BigHP, MedHP, SmaHP, BigMP, MedMP, SmaMP, Rose, Colesa, Balanda, PlayBoy }
        public Type type { get; protected set; }
        public Vector2 pos { get; protected set; }
        public enum Size { Little = 10, Middle = 25, Big = 50 }
        public uint power;
        public uint max_power;
        public bool renewable;
        private DateTime prev_recharge;
        public Artefact(uint max_power, bool renewable, bool powerable, bool choosable, Vector2 pos)
        {
            prev_recharge = DateTime.Now;
            this.max_power = max_power;
            power = max_power;
            this.renewable = renewable;
            this.powerable = powerable;
            this.choosable = choosable;
            this.pos = pos;
        }
        public void Update()
        {
            if(renewable && power < max_power && (DateTime.Now - prev_recharge).Seconds > 10)
            {
                prev_recharge = DateTime.Now;
                power++;
            }
        }
        public void Use(Character user)
        {
            Use(user, user, 1);
        }
        public void Use(Character user, Person_Abstract character)
        {
            Use(user, character, 1);
        }
        public void Use(Character user, uint power)
        {
            Use(user, user, power);
        }
        public virtual void Use(Character user, Person_Abstract character, uint power)
        {

        }

        public virtual void Draw(SpriteBatch spritebatch) { }
    }

    abstract class Spell : IMagic
    {
        public bool powerable { get; set; }
        public bool choosable { get; set; }
        public enum Type { AddHp, Heal, Move, Antidote, Shield, Revieve };
        public Type type { get; protected set; }
        public uint MinMana { get; protected set; }
        public bool canSpeakRequired { get; protected set; }
        public bool canMoveRequired { get; protected set; }
        protected Spell(uint minMana, bool canSpeakRequired, bool canMoveRequired, bool powerable, bool choosable)
        {
            this.MinMana = minMana;
            this.canSpeakRequired = canSpeakRequired;
            this.canMoveRequired = canMoveRequired;
            this.powerable = powerable;
            this.choosable = choosable;
        }
        public void Use(Character user)
        {
            Use(user, user, 1);
        }
        public void Use(Character user, Person_Abstract character)
        {
            Use(user, character, 1);
        }
        public void Use(Character user, uint power)
        {
            Use(user, user, power);
        }
        public virtual void Use(Character user, Person_Abstract character, uint power)
        { }
    }


}
