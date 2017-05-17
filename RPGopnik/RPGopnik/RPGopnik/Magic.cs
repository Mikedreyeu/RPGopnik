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
        void Use(double Power);
        void Use(Character character, double Power);
    }

    class Artefact : IMagic
    {
        protected double power;
        protected bool renewable;
        public virtual void Use() { }
        public virtual void Use(Character character) { }
        public virtual void Use(double Power) { }
        public virtual void Use(Character character, double Power) { }
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

        public override void Use(Character character, double Power)
        {

        }
    }
}
