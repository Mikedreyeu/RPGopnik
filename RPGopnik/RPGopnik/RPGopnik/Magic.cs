using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGopnik
{
    interface IMagic
    {
        void Use();
        void Use(Character character);
        void Use(double power);
        void Use(Character character, double power);
    }

    abstract class Artefact : IMagic
    {
        protected double power;
        protected bool renewable;
        public virtual void Use() { }
        public virtual void Use(Character character) { }
        public virtual void Use(double power) { }
        public virtual void Use(Character character, double power) { }
    }

    abstract class Spell : IMagic
    {
        protected uint minMana;
        protected bool canSpeakRequired;
        protected bool canMoveRequired;
        protected Spell(uint minMana, bool canSpeakRequired, bool canMoveRequired)
        {
            this.minMana = minMana;
            this.canSpeakRequired = canSpeakRequired;
            this.canMoveRequired = canMoveRequired;
        }
        public virtual void Use() { }
        public virtual void Use(Character character) { }
        public virtual void Use(double power) { }
        public virtual void Use(Character character, double power)
        {
            if (power <= 0)
                throw new ArgumentException("Power must be greater than zero.");
            if (character == null)
                throw new ArgumentException("._.");
        }
    }

    class Pivas : Artefact
    {
        public override void Use(Character character)
        {
            if (character.Curr_HP + power < character.Max_HP)
                character.Curr_HP = character.Curr_HP + (uint)power;
            else
                character.Curr_HP = character.Max_HP;
        }

        public override void Use(Character character, double power)
        {

        }
    }

    class AddHpSpell : Spell
    {
        public AddHpSpell() : base(2, true, true)
        {
            
        }

        public override void Use(Character character, double power)
        {
            base.Use(character, power);

            //как-то посмотреть ману того, кто кастует

            if (character.Curr_HP + power < character.Max_HP)
                character.Curr_HP = character.Curr_HP + (uint) power;
            else
                character.Curr_HP = character.Max_HP;
        }
    }
}
